using AgendaApp.Domain.Entities;

namespace AgendaApp.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByIdAsync(Guid id);
        Task AddAsync(User user);
    }
} 