using System.ComponentModel.DataAnnotations;

namespace BooksAPI.Models
{
    public class Quote
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Text { get; set; } = string.Empty;


        // Foreign key
        public int UserId { get; set; }

        // Navigation property
        public AppUser? User { get; set; }
    }
}