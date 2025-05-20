using System.ComponentModel.DataAnnotations;

namespace BooksAPI.Models.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Author { get; set; } = string.Empty;

        [Required]
        public DateTime PublicationDate { get; set; }

    }
}
