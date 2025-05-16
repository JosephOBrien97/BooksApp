using BooksAPI.Data.Repositories;
using BooksAPI.Models.DTOs;
using BooksAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class QuotesController : ControllerBase
    {
        private readonly IQuoteRepository _quoteRepository;

        public QuotesController(IQuoteRepository quoteRepository)
        {
            _quoteRepository = quoteRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuoteDto>>> GetUserQuotes()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var quotes = await _quoteRepository.GetQuotesByUserIdAsync(int.Parse(userId));
            var quoteDtos = new List<QuoteDto>();

            foreach (var quote in quotes)
            {
                quoteDtos.Add(new QuoteDto
                {
                    Id = quote.Id,
                    Text = quote.Text,
                    Author = quote.Author
                });
            }

            return Ok(quoteDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuoteDto>> GetQuote(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var quote = await _quoteRepository.GetQuoteByIdAsync(id);

            if (quote == null)
            {
                return NotFound();
            }

            if (quote.UserId != int.Parse(userId))
            {
                return Forbid();
            }

            var quoteDto = new QuoteDto
            {
                Id = quote.Id,
                Text = quote.Text,
                Author = quote.Author
            };

            return Ok(quoteDto);
        }

        [HttpPost]
        public async Task<ActionResult<QuoteDto>> CreateQuote(QuoteDto quoteDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            // Get user's current quotes count using repository method for efficiency
            var userQuotesCount = await _quoteRepository.GetUserQuotesCountAsync(int.Parse(userId));
            if (userQuotesCount >= 5)
            {
                return BadRequest(new { Message = "Maximum of 5 quotes allowed per user" });
            }

            var quote = new Quote
            {
                Text = quoteDto.Text,
                Author = quoteDto.Author,
                Source = quoteDto.Source,
                UserId = int.Parse(userId),
                CreatedAt = DateTime.UtcNow
            };

            var added = await _quoteRepository.AddQuoteAsync(quote);
            if (!added)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Failed to add quote" });
            }

            quoteDto.Id = quote.Id;
            quoteDto.CreatedAt = quote.CreatedAt;

            return CreatedAtAction(nameof(GetQuote), new { id = quote.Id }, quoteDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuote(int id, QuoteDto quoteDto)
        {
            if (id != quoteDto.Id)
            {
                return BadRequest();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var existingQuote = await _quoteRepository.GetQuoteByIdAsync(id);

            if (existingQuote == null)
            {
                return NotFound();
            }

            if (existingQuote.UserId != int.Parse(userId))
            {
                return Forbid();
            }

            existingQuote.Text = quoteDto.Text;
            existingQuote.Author = quoteDto.Author;

            await _quoteRepository.UpdateQuoteAsync(existingQuote);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuote(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var quote = await _quoteRepository.GetQuoteByIdAsync(id);

            if (quote == null)
            {
                return NotFound();
            }

            if (quote.UserId != int.Parse(userId))
            {
                return Forbid();
            }

            await _quoteRepository.DeleteQuoteAsync(id);

            return NoContent();
        }
    }
}
