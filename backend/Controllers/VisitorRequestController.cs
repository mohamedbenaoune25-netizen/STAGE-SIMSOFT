using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using backend.Services;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorRequestController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;

        public VisitorRequestController(AppDbContext context, IEmailService emailService, IWebHostEnvironment env)
        {
            _context = context;
            _emailService = emailService;
            _env = env;
        }

        // GET: api/VisitorRequest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VisitorRequest>>> GetVisitorRequests()
        {
            return await _context.VisitorRequests.OrderByDescending(r => r.CreatedAt).ToListAsync();
        }

        // POST: api/VisitorRequest
        [HttpPost]
        public async Task<ActionResult<VisitorRequest>> PostVisitorRequest(VisitorRequest visitorRequest)
        {
            visitorRequest.CreatedAt = DateTime.UtcNow;
            visitorRequest.Status = "Nouveau"; // Ensure default is set correctly
            
            _context.VisitorRequests.Add(visitorRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVisitorRequest", new { id = visitorRequest.Id }, visitorRequest);
        }

        // GET: api/VisitorRequest/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VisitorRequest>> GetVisitorRequest(int id)
        {
            var visitorRequest = await _context.VisitorRequests.FindAsync(id);

            if (visitorRequest == null)
            {
                return NotFound();
            }

            return visitorRequest;
        }

        // PUT: api/VisitorRequest/5/status
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string newStatus)
        {
            var visitorRequest = await _context.VisitorRequests.FindAsync(id);
            if (visitorRequest == null)
            {
                return NotFound();
            }

            visitorRequest.Status = newStatus;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        public class ReplyModel
        {
            public string Message { get; set; } = string.Empty;
        }

        // POST: api/VisitorRequest/5/reply
        [HttpPost("{id}/reply")]
        public async Task<IActionResult> ReplyToRequest(int id, [FromBody] ReplyModel reply)
        {
            var visitorRequest = await _context.VisitorRequests.FindAsync(id);
            if (visitorRequest == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(visitorRequest.VisitorEmail))
            {
                return BadRequest("Visitor email is missing.");
            }

            string subject = $"Re: {visitorRequest.Subject}";
            
            // Read HTML template
            string templatePath = Path.Combine(AppContext.BaseDirectory, "Templates", "ReplyTemplate.html");
            string body = string.Empty;
            if (System.IO.File.Exists(templatePath))
            {
                body = await System.IO.File.ReadAllTextAsync(templatePath);
                body = body.Replace("{{VisitorName}}", visitorRequest.VisitorName)
                           .Replace("{{Subject}}", visitorRequest.Subject)
                           .Replace("{{MessageBody}}", reply.Message.Replace("\n", "<br>"));
            }
            else
            {
                // Fallback to simple HTML if template not found
                body = $"<p>Bonjour {visitorRequest.VisitorName},</p><p>{reply.Message.Replace("\n", "<br>")}</p><br/><p>Cordialement,<br/>SimSoft Technologies</p>";
                Console.WriteLine($"Template not found at: {templatePath}");
            }

            await _emailService.SendEmailAsync(visitorRequest.VisitorEmail, subject, body);

            // Update status to solved or replied
            visitorRequest.Status = "Résolu";
            await _context.SaveChangesAsync();

            return Ok(new { message = "Reply sent successfully." });
        }

        // DELETE: api/VisitorRequest/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisitorRequest(int id)
        {
            var visitorRequest = await _context.VisitorRequests.FindAsync(id);
            if (visitorRequest == null)
            {
                return NotFound();
            }

            _context.VisitorRequests.Remove(visitorRequest);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
