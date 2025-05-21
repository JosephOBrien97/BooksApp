using System.ComponentModel.DataAnnotations;

namespace BooksAPI.Models.DTOs;

public class QuoteDto
{
    public int Id { get; set; }

    [Required] [StringLength(500)] public string Text { get; set; } = string.Empty;
}