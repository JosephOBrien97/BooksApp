using BooksAPI.Models;

namespace BooksAPI.Repositories.Interfaces;

public interface IQuoteRepository
{
    Task<IEnumerable<Quote>> GetAllQuotesAsync();
    Task<Quote?> GetQuoteByIdAsync(int id);
    Task<IEnumerable<Quote>> GetQuotesByUserIdAsync(int userId);
    Task<int> GetUserQuotesCountAsync(int userId);
    Task<bool> AddQuoteAsync(Quote quote);
    Task<bool> UpdateQuoteAsync(Quote quote);
    Task<bool> DeleteQuoteAsync(int quoteId);
    Task<bool> QuoteExistsAsync(int quoteId);
    Task<bool> UserOwnsQuoteAsync(int userId, int quoteId);
}