// AgendaApp.Application/DTOs/UserDtos.cs
public class LoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class UserResponseDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Nome { get; set; }
    public string Token { get; set; }
}