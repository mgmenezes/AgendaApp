using AgendaApp.Application.MessageBus.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AgendaApp.Application.Services;
using AgendaApp.Domain.Entities;
using AgendaApp.Domain.Interfaces;

namespace AgendaApp.Application.Commands.Contatos.CriarContato
{
    public class CriarContatoCommand : IRequest<Guid>
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }

    public class CriarContatoCommandHandler : IRequestHandler<CriarContatoCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessageBusService _messageBus;

        public CriarContatoCommandHandler(IUnitOfWork unitOfWork, IMessageBusService messageBus)
        {
            _unitOfWork = unitOfWork;
            _messageBus = messageBus;
        }

        public async Task<Guid> Handle(CriarContatoCommand request, CancellationToken cancellationToken)
        {
            var contato = new Contato(request.Nome, request.Email, request.Telefone);
            await _unitOfWork.ContatoRepository.AddAsync(contato);
            await _unitOfWork.CommitAsync();

            _messageBus.PublishMessage("contatos.criado", contato);
            return contato.Id;
        }
    }
} 