using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AgendaApp.Domain.Interfaces;

public interface IAuthService
{
    Task<UserResponseDto> LoginAsync(LoginDto loginDto);
}

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        IConfiguration configuration,
        IUserRepository userRepository,
        ILogger<AuthService> logger)
    {
        _configuration = configuration;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<UserResponseDto> LoginAsync(LoginDto loginDto)
    {
        // Log para debug
        _logger.LogInformation($"Tentando autenticar usuário: {loginDto.Email}");

        var user = await _userRepository.GetByEmailAsync(loginDto.Email);
        if (user == null)
        {
            _logger.LogWarning($"Usuário não encontrado: {loginDto.Email}");
            throw new UnauthorizedAccessException("Email ou senha incorretos");
        }

        if (user.PasswordHash != loginDto.Password)
        {
            _logger.LogWarning($"Senha incorreta para o usuário: {loginDto.Email}");
            throw new UnauthorizedAccessException("Email ou senha incorretos");
        }

        var token = GenerateJwtToken(user);

        return new UserResponseDto
        {
            Id = user.Id,
            Email = user.Email,
            Nome = user.Nome,
            Token = token
        };
    }

    private string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "chave_secreta_temporaria_para_teste"));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"] ?? "AgendaApp",
            audience: _configuration["Jwt:Audience"] ?? "AgendaApp",
            claims: new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            },
            expires: DateTime.Now.AddHours(3),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}