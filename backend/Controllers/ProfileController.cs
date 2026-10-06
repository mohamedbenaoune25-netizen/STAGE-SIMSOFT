using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using backend.Data;
using System.Security.Claims;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProfileController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/profile/me
        [HttpGet("me")]
        public IActionResult GetProfile()
        {
            var email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                     ?? User.FindFirst("sub")?.Value;

            if (email == null) return Unauthorized();

            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null) return NotFound();

            return Ok(new { user.Id, user.FullName, user.Email, user.Role, user.CreatedAt });
        }

        // PUT: api/profile/update-name
        [HttpPut("update-name")]
        public IActionResult UpdateName([FromBody] UpdateNameDto dto)
        {
            var email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                     ?? User.FindFirst("sub")?.Value;

            if (email == null) return Unauthorized();

            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null) return NotFound();

            if (string.IsNullOrWhiteSpace(dto.FullName) || dto.FullName.Trim().Length < 2)
                return BadRequest(new { message = "Le nom doit contenir au moins 2 caractères." });

            user.FullName = dto.FullName.Trim();
            _context.SaveChanges();

            return Ok(new { message = "Nom mis à jour avec succès.", fullName = user.FullName });
        }

        // PUT: api/profile/update-password
        [HttpPut("update-password")]
        public IActionResult UpdatePassword([FromBody] UpdatePasswordDto dto)
        {
            var email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                     ?? User.FindFirst("sub")?.Value;

            if (email == null) return Unauthorized();

            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null) return NotFound();

            // Verify current password
            if (!BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, user.PasswordHash))
                return BadRequest(new { message = "Le mot de passe actuel est incorrect." });

            // Validate new password
            if (string.IsNullOrWhiteSpace(dto.NewPassword) || dto.NewPassword.Length < 8)
                return BadRequest(new { message = "Le nouveau mot de passe doit contenir au moins 8 caractères." });

            if (dto.NewPassword == dto.CurrentPassword)
                return BadRequest(new { message = "Le nouveau mot de passe doit être différent de l'ancien." });

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            _context.SaveChanges();

            return Ok(new { message = "Mot de passe mis à jour avec succès." });
        }
    }

    public class UpdateNameDto
    {
        public string FullName { get; set; } = string.Empty;
    }

    public class UpdatePasswordDto
    {
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
