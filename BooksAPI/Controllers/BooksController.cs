using BooksAPI.Models;
using BooksAPI.Models.DTOs;
using BooksAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository context)
        {
            _bookRepository = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAllBooks()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            var bookDto = books.Select(book => new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                PublicationDate = book.PublicationDate
            })
                .ToList();

            return Ok(bookDto);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            var bookDto = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                PublicationDate = book.PublicationDate
            };

            return Ok(bookDto);
        }
        [HttpPost]
        public async Task<ActionResult<BookDto>> AddBook(BookDto bookDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var book = new Book
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                PublicationDate = bookDto.PublicationDate,
                UserId = int.Parse(userId)
            };

            await _bookRepository.AddBookAsync(book);

            bookDto.Id = book.Id;

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, bookDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, BookDto bookDto)
        {
            if (id != bookDto.Id)
            {
                return BadRequest();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var existingBook = await _bookRepository.GetBookByIdAsync(id);

            if (existingBook == null)
            {
                return NotFound();
            }

            if (existingBook.UserId != int.Parse(userId))
            {
                return Forbid();
            }

            existingBook.Title = bookDto.Title;
            existingBook.Author = bookDto.Author;
            existingBook.PublicationDate = bookDto.PublicationDate;

            await _bookRepository.UpdateBookAsync(existingBook);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var book = await _bookRepository.GetBookByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            if (book.UserId != int.Parse(userId))
            {
                return Forbid();
            }

            await _bookRepository.DeleteBookAsync(id);

            return NoContent();
        }
    }
}
