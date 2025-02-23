namespace AgendaApp.Application.MessageBus.Interfaces
{
    public interface IMessageBusService
    {
        void PublishMessage<T>(string queue, T message);
        void Subscribe<T>(string queue, Action<T> handler);
    }
}