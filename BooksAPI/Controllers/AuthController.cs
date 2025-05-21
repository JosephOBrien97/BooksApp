using BooksAPI.Models;
using BooksAPI.Models.DTOs;
using BooksAPI.Repositories.Interfaces;
using BooksAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IJwtService _jwtService;
    private readonly IUserRepository _userRepository;

    public AuthController(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (await _userRepository.GetUserByUsernameAsync(registerDto.Username) != null)
            return BadRequest(new { Message = "Username already exists" });

        var user = new AppUser
        {
            Username = registerDto.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
        };

        await _userRepository.AddUserAsync(user);

        var token = _jwtService.GenerateToken(user);
        return Ok(new { Token = token });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await _userRepository.GetUserByUsernameAsync(loginDto.Username);
        if (user == null) return Unauthorized(new { Message = "Invalid username or password" });

        var isValidPassword = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
        if (!isValidPassword) return Unauthorized(new { Message = "Invalid username or password" });

        var token = _jwtService.GenerateToken(user);
        return Ok(new { Token = token });
    }
}