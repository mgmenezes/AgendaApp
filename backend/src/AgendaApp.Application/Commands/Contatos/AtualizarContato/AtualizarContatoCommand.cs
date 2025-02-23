using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using AgendaApp.Domain.Entities;
using AgendaApp.Domain.Interfaces;

namespace AgendaApp.Application.Commands.Contatos.AtualizarContato
{
    public class AtualizarContatoCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }

    public class AtualizarContatoCommandHandler : IRequestHandler<AtualizarContatoCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AtualizarContatoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(AtualizarContatoCommand request, CancellationToken cancellationToken)
        {
            var contato = await _unitOfWork.ContatoRepository.GetByIdAsync(request.Id);
            if (contato == null) return false;

            _mapper.Map(request, contato);

            await _unitOfWork.ContatoRepository.UpdateAsync(contato);
            await _unitOfWork.CommitAsync();

            return true;
        }
    }
} 