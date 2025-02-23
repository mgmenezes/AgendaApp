using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using AgendaApp.Application.DTOs;
using AgendaApp.Application.Services;
using AgendaApp.Domain.Interfaces;
namespace AgendaApp.Application.Queries.Contatos.ObterContatoPorId
{
    public class ObterContatoPorIdQuery : IRequest<ContatoDto>
    {
        public Guid Id { get; set; }
    }

    public class ObterContatoPorIdQueryHandler : IRequestHandler<ObterContatoPorIdQuery, ContatoDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ObterContatoPorIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ContatoDto> Handle(ObterContatoPorIdQuery request, CancellationToken cancellationToken)
        {
            var contato = await _unitOfWork.ContatoRepository.GetByIdAsync(request.Id);
            return _mapper.Map<ContatoDto>(contato);
        }
    }
} 