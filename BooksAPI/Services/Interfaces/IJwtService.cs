using BooksAPI.Models;

namespace BooksAPI.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(AppUser user);
    }
}
