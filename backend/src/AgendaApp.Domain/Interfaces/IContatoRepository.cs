// AgendaApp.Domain/Interfaces/IContatoRepository.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgendaApp.Domain.Entities;

namespace AgendaApp.Domain.Interfaces
{
    public interface IContatoRepository
    {
        Task<IEnumerable<Contato>> GetAllAsync();
        Task<Contato> GetByIdAsync(Guid id);
        Task AddAsync(Contato contato);
        Task UpdateAsync(Contato contato);
        Task DeleteAsync(Guid id);
        Task<Contato?> ObterPorIdAsync(Guid id);  // Adicionando o ? para indicar que pode ser nulo
        Task<IEnumerable<Contato>> ObterTodosAsync();
        Task<IEnumerable<Contato>> ObterAtivosAsync();
        Task<Contato?> ObterPorEmailAsync(string email);  // Adicionando o ? aqui também
        Task<Contato> AdicionarAsync(Contato contato);
        Task AtualizarAsync(Contato contato);
        Task<bool> ExisteEmailAsync(string email);
        Task InativarAsync(Guid id);
    }
}