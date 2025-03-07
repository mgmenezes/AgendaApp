namespace AgendaApp.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IContatoRepository ContatoRepository { get; }
        Task<bool> CommitAsync();
    }
}