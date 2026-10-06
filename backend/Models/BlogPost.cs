using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class BlogPost
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        public string Content { get; set; } = string.Empty;
        
        [MaxLength(100)]
        public string Author { get; set; } = string.Empty;
        
        public string ImageUrl { get; set; } = string.Empty;
        
        public string Category { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }
        
        public bool IsPublished { get; set; } = false;
    }
}
