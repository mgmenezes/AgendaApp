using AgendaApp.Domain.Interfaces;
using AgendaApp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AgendaApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AgendaContext _context;

        public UserRepository(AgendaContext context)
        {
            _context = context;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}