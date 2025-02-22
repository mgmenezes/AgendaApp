// AgendaApp.Domain/Entities/Contato.cs
using System;
using AgendaApp.Domain.Exceptions;

namespace AgendaApp.Domain.Entities
{
    public class Contato
    {
        public Guid Id { get; set ; }
        public string Nome { get; set ; } = string.Empty;    
        public string Email { get; set ; } = string.Empty;
        public string Telefone { get; set ; } = string.Empty;
        public DateTime DataCriacao { get; set ; } = DateTime.UtcNow;
        public DateTime? DataAtualizacao { get; set ; }
        public bool Ativo { get; set ; } = true;        
    }
}