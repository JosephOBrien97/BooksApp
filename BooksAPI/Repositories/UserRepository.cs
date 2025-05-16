using BooksAPI.Data;
using BooksAPI.Models;
using BooksAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BookAppDb _context;

        public UserRepository(BookAppDb context)
        {
            _context = context;
        }
        public async Task<AppUser?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .SingleOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
        }

        public async Task<AppUser?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .SingleOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<bool> UserExistsAsync(string username, string email)
        {
            return await _context.Users.AnyAsync(u => 
                u.Username.ToLower() == username.ToLower() || 
                u.Email.ToLower() == email.ToLower());
        }

        public async Task<bool> AddUserAsync(AppUser user)
        {
            await _context.Users.AddAsync(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUserAsync(AppUser user)
        {
            _context.Users.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
           _context.Users.Remove(new AppUser { Id = id });
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
