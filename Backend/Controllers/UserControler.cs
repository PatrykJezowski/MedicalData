using Backend.DTO.User;
using Microsoft.AspNetCore.Mvc; // potrzebne do ControllerBase, IActionResult

[ApiController] // oznacza że to kontroler API (automatyczna walidacja, binding itd.)
[Route("api/[controller]")] // endpoint: /api/users
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    // serwis z logiką (nie robimy logiki w kontrolerze!)

    public UserController(UserService userService)
    {
        _userService = userService;
        // DI (Dependency Injection) — ASP.NET sam wstrzykuje serwis
    }

    [HttpPost("register")] // endpoint: POST /api/users/register
    public async Task<IActionResult> Register(RegisterUserDto dto)
    {
        try
        {
            var user = await _userService.RegisterAsync(dto);
            // wywołujemy logikę rejestracji

            return Ok(new
            {
                user.Id,
                user.Email
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")] // endpoint: POST /api/user/login
    public async Task<IActionResult> Login(LoginUserDto dto)
    {
        var token = await _userService.LoginAsync(dto);
        // próbujemy się zalogować

        if (token == null)
        {
            return Unauthorized("Invalid email or password");
            // 401 jeśli login nieudany
        }

        return Ok(new
        {
            token
        });
        // login OK → zwracamy dane usera
    }
}