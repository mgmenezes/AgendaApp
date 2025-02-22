// AgendaApp.Domain/Entities/Contato.cs
using System;
using AgendaApp.Domain.Exceptions;

namespace AgendaApp.Domain.Entities
{
    public class Contato
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Telefone { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataAtualizacao { get; private set; }
        public bool Ativo { get; private set; }

        // Construtor protegido para o EF Core
        protected Contato()
        {
            Nome = string.Empty;
            Email = string.Empty;
            Telefone = string.Empty;
            Id = Guid.Empty;
            DataCriacao = DateTime.MinValue;
            Ativo = false;
        }

        // Construtor público para criar novos contatos
        public Contato(string nome, string email, string telefone)
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.UtcNow;
            Ativo = true;
            AtualizarDados(nome, email, telefone);
        }

        public void AtualizarDados(string nome, string email, string telefone)
        {
            ValidarDados(nome, email, telefone);
            
            Nome = nome;
            Email = email;
            Telefone = telefone;
            DataAtualizacao = DateTime.UtcNow;
        }

        private void ValidarDados(string nome, string email, string telefone)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new DomainException("O nome não pode estar vazio");

            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("O email não pode estar vazio");

            if (string.IsNullOrWhiteSpace(telefone))
                throw new DomainException("O telefone não pode estar vazio");
        }

        public void Inativar()
        {
            Ativo = false;
            DataAtualizacao = DateTime.UtcNow;
        }
    }
}