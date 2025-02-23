// AgendaApp.Domain/Entities/User.cs
public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string Nome { get; private set; }

    protected User() { }

    public User(string email, string passwordHash, string nome)
    {
        Id = Guid.NewGuid();
        Email = email;
        PasswordHash = passwordHash;
        Nome = nome;
    }
}