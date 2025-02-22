// AgendaApp.Application/Exceptions/NotFoundException.cs
namespace AgendaApp.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}