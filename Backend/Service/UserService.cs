using Backend.Data;
using Backend.DTO.User;
using Backend.Models;
using Backend.Service;

public class UserService 
{
    private readonly MedicalContext _context;
    private readonly PasswordService _passwordService;

    public UserService(MedicalContext context, PasswordService passwordService)
    {
        _context = context;

        _passwordService = passwordService;
    }

    public async Task<User> RegisterAsync(RegisterUserDto dto)
    // metoda rejestracji użytkownika
    {
        if (_context.Users.Any(u => u.Email == dto.Email))
        // sprawdzamy czy istnieje user z tym emailem
        // Any = szybkie sprawdzenie (zwraca true/false)
        {
            throw new Exception("Email already exists");
            // jeśli tak → blokujemy rejestrację
        }

        var user = new User
        {
            Email = dto.Email
            // tworzymy nowy obiekt użytkownika
            // przypisujemy email z DTO (czyli z requesta)
        };

        user.HashedPassword = _passwordService.Hash(dto.Password);
        // haszujemy hasło i zapisujemy je w modelu
        // NIE zapisujemy plain text password!

        _context.Users.Add(user);
        // dodajemy użytkownika do EF (jeszcze nie zapisuje do bazy)

        await _context.SaveChangesAsync();
        // zapis do bazy danych (tu dopiero idzie SQL)

        return user;
        // zwracamy stworzonego usera (np. do kontrolera)
    }

    public async Task<User?> LoginAsync(LoginUserDto dto)
    // metoda logowania
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email);
        // szukamy usera po emailu
        // FirstOrDefault = zwraca null jeśli nie ma

        if (user == null)
        {
            return null;
            // jeśli nie znaleziono → login nieudany
        }

        var isValid = _passwordService.Verify(user, dto.Password, user.HashedPassword);
        // sprawdzamy:
        // - hasło z requesta
        // - hash z bazy

        if (!isValid)
        {
            return null;
            // jeśli hasło złe → login nieudany
        }

        return user;
        // jeśli wszystko OK → zwracamy usera (login udany)
    }
}