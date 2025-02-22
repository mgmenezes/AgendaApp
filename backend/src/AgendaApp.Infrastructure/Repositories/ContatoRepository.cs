// AgendaApp.Infrastructure/Repositories/ContatoRepository.cs
using AgendaApp.Domain.Entities;
using AgendaApp.Domain.Interfaces;
using AgendaApp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AgendaApp.Infrastructure.Repositories
{
    public class ContatoRepository : IContatoRepository
    {
        private readonly AgendaContext _context;

        public ContatoRepository(AgendaContext context)
        {
            _context = context;
        }

        public async Task<Contato?> ObterPorIdAsync(Guid id)
        {
            return await _context.Contatos
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Contato>> ObterTodosAsync()
        {
            return await _context.Contatos
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Contato>> ObterAtivosAsync()
        {
            return await _context.Contatos
                .AsNoTracking()
                .Where(c => c.Ativo)
                .ToListAsync();
        }

            public async Task<Contato?> ObterPorEmailAsync(string email)
        {
            return await _context.Contatos
                .FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<Contato> AdicionarAsync(Contato contato)
        {
            await _context.Contatos.AddAsync(contato);
            return contato;
        }

         public async Task AtualizarAsync(Contato contato)
        {
            _context.Entry(contato).State = EntityState.Modified;
            await Task.CompletedTask; // Resolvendo o warning do método assíncrono
        }

        public async Task<bool> ExisteEmailAsync(string email)
        {
            return await _context.Contatos
                .AnyAsync(c => c.Email == email);
        }

        public async Task InativarAsync(Guid id)
        {
            var contato = await ObterPorIdAsync(id);
            if (contato != null)
            {
                contato.Inativar();
                _context.Contatos.Update(contato);
            }
        }
    }
}