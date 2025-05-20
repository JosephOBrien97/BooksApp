using System.ComponentModel.DataAnnotations;

namespace BooksAPI.Models
{
    public class AppUser
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(75, MinimumLength = 5)]
        public string PasswordHash { get; set; } = string.Empty;

        // Navigation property
        public ICollection<Book>? Books { get; set; }
        public ICollection<Quote>? Quotes { get; set; }
    }
}
