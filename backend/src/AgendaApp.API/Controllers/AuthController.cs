using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserResponseDto>> Login(LoginDto loginDto)
    {
        try
        {
            _logger.LogInformation($"Tentativa de login para o email: {loginDto.Email}");

            var result = await _authService.LoginAsync(loginDto);

            _logger.LogInformation($"Login bem-sucedido para o email: {loginDto.Email}");
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning($"Falha no login para o email: {loginDto.Email} - {ex.Message}");
            return Unauthorized(new { message = "Email ou senha incorretos" });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro no login: {ex.Message}");
            return StatusCode(500, new { message = "Erro interno do servidor" });
        }
    }
}