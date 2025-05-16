using BooksAPI.Models;

namespace BooksAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<AppUser?> GetUserByIdAsync(int id);
        Task<AppUser?> GetUserByUsernameAsync(string username);
        Task<AppUser?> GetUserByEmailAsync(string email);
        Task<bool> UserExistsAsync(string username, string email);
        Task<bool> AddUserAsync(AppUser user);
        Task<bool> UpdateUserAsync(AppUser user);
        Task<bool> DeleteUserAsync(int id);
    }
}
