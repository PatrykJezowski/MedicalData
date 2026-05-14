using Backend.Data;
using Backend.DTO.User;
using Backend.Models;
using Backend.Service;

public class UserService 
{
    private readonly MedicalContext _context;
    private readonly PasswordService _passwordService;
    private readonly JwtService _jwtService;

    public UserService(MedicalContext context, PasswordService passwordService, JwtService jwtService)
    {
        _context = context;

        _passwordService = passwordService;
        _jwtService = jwtService;
    }

    public async Task<User> RegisterAsync(RegisterUserDto dto)
    {
        if (_context.User.Any(u => u.Email == dto.Email))
        {
            throw new Exception("Email already exists");
        }

        var user = new User
        {
            Email = dto.Email
        };

        user.HashedPassword = _passwordService.Hash(dto.Password);

        _context.User.Add(user);
        // dodajemy użytkownika do EF (jeszcze nie zapisuje do bazy)

        await _context.SaveChangesAsync();
        // zapis do bazy danych (tu dopiero idzie SQL)

        return user;
    }

    public async Task<string?> LoginAsync(LoginUserDto dto)
    {
        var user = _context.User.FirstOrDefault(u => u.Email == dto.Email);

        if (user == null)
        {
            return null;
        }

        var isValid = _passwordService.Verify(user, dto.Password, user.HashedPassword);

        if (!isValid)
        {
            return null;
        }

        return _jwtService.CreateToken(user);
    }
}