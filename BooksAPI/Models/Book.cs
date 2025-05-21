using System.ComponentModel.DataAnnotations;

namespace BooksAPI.Models;

public class Book
{
    public int Id { get; set; }

    [Required] [StringLength(200)] public string Title { get; set; } = string.Empty;

    [Required] [StringLength(100)] public string Author { get; set; } = string.Empty;

    public DateTime PublicationDate { get; set; }

    // Foreign key
    public int UserId { get; set; }

    // Navigation property
    public AppUser? User { get; set; }
}