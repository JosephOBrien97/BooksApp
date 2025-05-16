using BooksAPI.Models;

namespace BooksAPI.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int id);
        Task<IEnumerable<Book>> GetBooksByUserIdAsync(int userId);
        Task<bool> AddBookAsync(Book book);
        Task<bool> UpdateBookAsync(Book book);
        Task<bool> DeleteBookAsync(int bookId);
        Task<bool> BookExistsAsync(int bookId);
        Task<bool> UserOwnsBookAsync(int userId, int bookId);
    }
}
