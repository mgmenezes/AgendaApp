namespace AgendaApp.Domain.Entities
{
    public class Contato
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Telefone { get; private set; }
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        public DateTime? DataAtualizacao { get; set; }
        public bool Ativo { get; set; } = true;

        public Contato(string nome, string email, string telefone)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
            Telefone = telefone;
        }
    }
}