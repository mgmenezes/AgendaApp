using AgendaApp.Domain.Interfaces;
using AgendaApp.Infrastructure.Context;
using AgendaApp.Infrastructure.Repositories;

namespace AgendaApp.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AgendaContext _context;
        private IContatoRepository? _contatoRepository;

        public UnitOfWork(AgendaContext context)
        {
            _context = context;
        }

        public IContatoRepository ContatoRepository
        {
            get => _contatoRepository ??= new ContatoRepository(_context);
        }

        public async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}