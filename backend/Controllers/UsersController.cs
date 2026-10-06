using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using backend.Data;
using backend.Models;
using System.Linq;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly backend.Services.IEmailService _emailService;

        public UsersController(AppDbContext context, backend.Services.IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // GET: api/users
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _context.Users
                .Select(u => new 
                { 
                    u.Id, 
                    u.FullName, 
                    u.Email, 
                    u.Role, 
                    u.CreatedAt, 
                    u.LastLogin, 
                    u.IsActive 
                })
                .ToList();
            return Ok(users);
        }

        // POST: api/users
        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
        {
            if (_context.Users.Any(u => u.Email == dto.Email))
            {
                return BadRequest(new { message = "Un utilisateur avec cet email existe déjà." });
            }

            var newUser = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Role = dto.Role,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                CreatedAt = System.DateTime.UtcNow,
                IsActive = true
            };
            
            _context.Users.Add(newUser);
            _context.SaveChanges();

            // Send Email
            try
            {
                var templatePath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Templates", "AdminCreationTemplate.html");
                if (System.IO.File.Exists(templatePath))
                {
                    var htmlMessage = await System.IO.File.ReadAllTextAsync(templatePath);
                    htmlMessage = htmlMessage.Replace("{{FullName}}", newUser.FullName);
                    htmlMessage = htmlMessage.Replace("{{Email}}", newUser.Email);
                    htmlMessage = htmlMessage.Replace("{{TempPassword}}", dto.Password);
                    htmlMessage = htmlMessage.Replace("{{LoginUrl}}", "http://localhost:4200/login");
                    
                    await _emailService.SendEmailAsync(newUser.Email, "Création de votre compte Administrateur", htmlMessage);
                }
            }
            catch (System.Exception ex)
            {
                // We log the error but still return success for user creation
                System.Console.WriteLine($"Erreur lors de l'envoi de l'email : {ex.Message}");
            }

            return Ok(new { message = "Utilisateur créé avec succès.", user = newUser });
        }

        // PUT: api/users/5/status
        [HttpPut("{id}/status")]
        public IActionResult ToggleStatus(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            if (user.Email == "mohamed.benaoune25@gmail.com")
            {
                return BadRequest(new { message = "Impossible de désactiver l'administrateur principal." });
            }

            user.IsActive = !user.IsActive;
            _context.SaveChanges();

            return Ok(new { message = "Statut mis à jour avec succès.", isActive = user.IsActive });
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            if (user.Email == "mohamed.benaoune25@gmail.com")
            {
                return BadRequest(new { message = "Impossible de supprimer l'administrateur principal." });
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return Ok(new { message = "Utilisateur supprimé avec succès." });
        }

        // PUT: api/users/5/role
        [HttpPut("{id}/role")]
        public IActionResult UpdateRole(int id, [FromBody] UpdateRoleDto dto)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            if (user.Email == "mohamed.benaoune25@gmail.com")
            {
                return BadRequest(new { message = "Impossible de modifier le rôle de l'administrateur principal." });
            }

            if (dto.Role != "Admin" && dto.Role != "Editeur")
            {
                return BadRequest(new { message = "Rôle invalide." });
            }

            user.Role = dto.Role;
            _context.SaveChanges();

            return Ok(new { message = "Rôle mis à jour avec succès.", role = user.Role });
        }
    }

    public class CreateUserDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
    }

    public class UpdateRoleDto
    {
        public string Role { get; set; }
    }
}
