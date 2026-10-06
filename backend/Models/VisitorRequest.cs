using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class VisitorRequest
    {
        public int Id { get; set; }

        [Required]
        public string VisitorName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string VisitorEmail { get; set; } = string.Empty;

        [Required]
        public string Subject { get; set; } = string.Empty;

        [Required]
        public string Message { get; set; } = string.Empty;

        public string Status { get; set; } = "Nouveau"; // Nouveau, En cours, Résolu

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
