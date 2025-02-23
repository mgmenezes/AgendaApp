using FluentValidation.Results;

namespace AgendaApp.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
            Errors = new List<ValidationFailure>();
        }

        public ValidationException(IEnumerable<ValidationFailure> errors)
            : base("Ocorreram erros de validação.")
        {
            Errors = errors;
        }

        public IEnumerable<ValidationFailure> Errors { get; }
    }
}