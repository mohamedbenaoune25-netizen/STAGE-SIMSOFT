using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Models;
using backend.Data;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        private readonly backend.Services.IEmailService _emailService;

        public AuthController(IConfiguration configuration, AppDbContext context, backend.Services.IEmailService emailService)
        {
            _configuration = configuration;
            _context = context;
            _emailService = emailService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == request.Username);
            
            if (user == null || !user.IsActive || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return Unauthorized(new { message = "Identifiants invalides ou compte désactivé." });
            }

            // Update last login
            user.LastLogin = DateTime.UtcNow;
            _context.SaveChanges();

            var token = GenerateJwtToken(user);
            return Ok(new { Token = token, Role = user.Role });
        }

        [HttpPost("change-password")]
        [Authorize]
        public IActionResult ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
            
            if (user == null) return NotFound("Utilisateur non trouvé.");
            
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            _context.SaveChanges();
            
            return Ok(new { message = "Mot de passe modifié avec succès." });
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["Secret"] ?? throw new InvalidOperationException("Secret manquant");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("FullName", user.FullName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("forgot-password")]
        public async System.Threading.Tasks.Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);
            
            // On retourne toujours un Ok pour des raisons de sécurité (ne pas fuiter les emails valides)
            if (user == null || !user.IsActive)
            {
                return Ok(new { message = "Si cet email correspond à un compte actif, un nouveau mot de passe a été envoyé." });
            }

            // Génération d'un mot de passe temporaire
            var tempPassword = GenerateTempPassword();
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(tempPassword);
            _context.SaveChanges();

            // Envoi de l'email
            try
            {
                var templatePath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Templates", "ResetPasswordTemplate.html");
                if (System.IO.File.Exists(templatePath))
                {
                    var htmlMessage = await System.IO.File.ReadAllTextAsync(templatePath);
                    htmlMessage = htmlMessage.Replace("{{FullName}}", user.FullName);
                    htmlMessage = htmlMessage.Replace("{{Email}}", user.Email);
                    htmlMessage = htmlMessage.Replace("{{TempPassword}}", tempPassword);
                    htmlMessage = htmlMessage.Replace("{{LoginUrl}}", "http://localhost:4200/login");
                    
                    await _emailService.SendEmailAsync(user.Email, "Réinitialisation de votre mot de passe", htmlMessage);
                }
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine($"Erreur lors de l'envoi de l'email de reset : {ex.Message}");
            }

            return Ok(new { message = "Si cet email correspond à un compte actif, un nouveau mot de passe a été envoyé." });
        }

        private string GenerateTempPassword()
        {
            const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowercase = "abcdefghijklmnopqrstuvwxyz";
            const string numbers = "0123456789";
            const string symbols = "!@#$%^&*()_+~|}{[]:;?><,./-=";
            const string allChars = uppercase + lowercase + numbers + symbols;

            var random = new Random();
            var password = new StringBuilder();
            
            password.Append(uppercase[random.Next(uppercase.Length)]);
            password.Append(lowercase[random.Next(lowercase.Length)]);
            password.Append(numbers[random.Next(numbers.Length)]);
            password.Append(symbols[random.Next(symbols.Length)]);

            for (int i = 4; i < 12; i++)
            {
                password.Append(allChars[random.Next(allChars.Length)]);
            }

            // Shuffle
            return new string(password.ToString().OrderBy(s => random.NextDouble()).ToArray());
        }
    }

    public class ForgotPasswordRequest
    {
        public string Email { get; set; }
    }
}
