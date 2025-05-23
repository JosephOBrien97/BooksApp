﻿using System.Security.Claims;
using BooksAPI.Models;
using BooksAPI.Models.DTOs;
using BooksAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.Controllers;

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
    public async Task<ActionResult<IEnumerable<QuoteDto>>> GetQuotes()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        var quotes = await _quoteRepository.GetQuotesByUserIdAsync(userId);
        var quoteDtos = new List<QuoteDto>();

        foreach (var quote in quotes)
            quoteDtos.Add(new QuoteDto
            {
                Id = quote.Id,
                Text = quote.Text
            });

        return Ok(quoteDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<QuoteDto>> GetQuote(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var quote = await _quoteRepository.GetQuoteByIdAsync(id);

        if (quote == null) return NotFound();

        if (quote.UserId != int.Parse(userId)) return Forbid();

        var quoteDto = new QuoteDto
        {
            Id = quote.Id,
            Text = quote.Text
        };

        return Ok(quoteDto);
    }

    [HttpPost]
    public async Task<ActionResult<QuoteDto>> AddQuote(QuoteDto quoteDto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        var userQuotesCount = await _quoteRepository.GetUserQuotesCountAsync(userId);
        if (userQuotesCount >= 5) return BadRequest(new { Message = "Maximum of 5 quotes allowed per user" });

        var quote = new Quote
        {
            Text = quoteDto.Text,
            UserId = userId
        };

        var added = await _quoteRepository.AddQuoteAsync(quote);
        if (!added)
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Failed to add quote" });

        quoteDto.Id = quote.Id;

        return CreatedAtAction(nameof(GetQuote), new { id = quote.Id }, quoteDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateQuote(int id, QuoteDto quoteDto)
    {
        if (id != quoteDto.Id) return BadRequest();

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var existingQuote = await _quoteRepository.GetQuoteByIdAsync(id);

        if (existingQuote == null) return NotFound();

        if (existingQuote.UserId != int.Parse(userId)) return Forbid();

        existingQuote.Text = quoteDto.Text;

        await _quoteRepository.UpdateQuoteAsync(existingQuote);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQuote(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var quote = await _quoteRepository.GetQuoteByIdAsync(id);

        if (quote == null) return NotFound();

        if (quote.UserId != int.Parse(userId)) return Forbid();

        await _quoteRepository.DeleteQuoteAsync(id);

        return NoContent();
    }
}