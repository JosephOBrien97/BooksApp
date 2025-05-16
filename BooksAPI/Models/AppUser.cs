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
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastActive { get; set; } = DateTime.UtcNow;

        // Navigation property
        public ICollection<Book>? Books { get; set; }
        public ICollection<Quote>? Quotes{ get; set; }
    }
}
