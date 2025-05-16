using BooksAPI.Models;

namespace BooksAPI.Services
{
    public interface IJwtService
    {
        string GenerateToken(AppUser user);
    }
}
