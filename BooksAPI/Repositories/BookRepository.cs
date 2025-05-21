using BooksAPI.Data;
using BooksAPI.Models;
using BooksAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Repositories;

public class BookRepository : IBookRepository
{
    private readonly BookAppDb _context;

    public BookRepository(BookAppDb context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _context.Books
            .Include(b => b.User)
            .ToListAsync();
    }

    public async Task<Book?> GetBookByIdAsync(int id)
    {
        return await _context.Books
            .Include(b => b.User)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<Book>> GetBooksByUserIdAsync(int userId)
    {
        return await _context.Books
            .Where(b => b.UserId == userId)
            .ToListAsync();
    }

    public async Task<bool> AddBookAsync(Book book)
    {
        await _context.Books.AddAsync(book);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateBookAsync(Book book)
    {
        _context.Books.Update(book);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteBookAsync(int bookId)
    {
        var book = await _context.Books.FindAsync(bookId);
        if (book == null)
            return false;

        _context.Books.Remove(book);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> BookExistsAsync(int bookId)
    {
        return await _context.Books.AnyAsync(b => b.Id == bookId);
    }

    public async Task<bool> UserOwnsBookAsync(int userId, int bookId)
    {
        return await _context.Books.AnyAsync(b => b.Id == bookId && b.UserId == userId);
    }
}