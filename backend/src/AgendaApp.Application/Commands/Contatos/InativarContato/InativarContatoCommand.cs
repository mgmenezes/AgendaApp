using MediatR;
using AgendaApp.Domain.Interfaces;

namespace AgendaApp.Application.Commands.Contatos.InativarContato
{
    public class InativarContatoCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class InativarContatoCommandHandler : IRequestHandler<InativarContatoCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public InativarContatoCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(InativarContatoCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.ContatoRepository.DeleteAsync(request.Id);
            await _unitOfWork.CommitAsync();
        }
    }
} 