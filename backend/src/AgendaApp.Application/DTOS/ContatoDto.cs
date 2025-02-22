// AgendaApp.Application/DTOs/ContatoDto.cs
namespace AgendaApp.Application.DTOs
{
    public class ContatoDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;  
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }

    public class CriarContatoDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
    }

    public class AtualizarContatoDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
    }
}