using BooksAPI.Data;
using BooksAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Data.Repositories
{
    public class QuoteRepository : IQuoteRepository
    {

        private readonly BookAppDb _context;

        public QuoteRepository(BookAppDb context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Quote>> GetAllQuotesAsync()
        {
            return await _context.Quotes
                .Include(q => q.User)
                .OrderByDescending(q => q.CreatedAt)
                .ToListAsync();
        }

        public async Task<Quote?> GetQuoteByIdAsync(int id)
        {
            return await _context.Quotes
                .Include(q => q.User)
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<IEnumerable<Quote>> GetQuotesByUserIdAsync(int userId)
        {
            return await _context.Quotes
                .Where(q => q.UserId == userId)
                .OrderByDescending(q => q.CreatedAt)
                .ToListAsync();
        }

        public async Task<int> GetUserQuotesCountAsync(int userId)
        {
            return await _context.Quotes.CountAsync(q => q.UserId == userId);
        }

        public async Task<bool> AddQuoteAsync(Quote quote)
        {
            await _context.Quotes.AddAsync(quote);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateQuoteAsync(Quote quote)
        {
            quote.UpdatedAt = DateTime.UtcNow;
            _context.Quotes.Update(quote);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteQuoteAsync(int quoteId)
        {
            var quote = await _context.Quotes.FindAsync(quoteId);
            if (quote == null)
                return false;

            _context.Quotes.Remove(quote);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> QuoteExistsAsync(int quoteId)
        {
            return await _context.Quotes.AnyAsync(q => q.Id == quoteId);
        }

        public async Task<bool> UserOwnsQuoteAsync(int userId, int quoteId)
        {
            return await _context.Quotes.AnyAsync(q => q.Id == quoteId && q.UserId == userId);
        }
    }
}
