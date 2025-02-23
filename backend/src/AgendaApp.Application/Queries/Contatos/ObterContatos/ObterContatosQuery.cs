using MediatR;
using AutoMapper;
using AgendaApp.Application.DTOs;
using AgendaApp.Domain.Interfaces;

namespace AgendaApp.Application.Queries.Contatos.ObterContatos
{
    public class ObterContatosQuery : IRequest<IEnumerable<ContatoDto>>
    {
    }

    public class ObterContatosQueryHandler : IRequestHandler<ObterContatosQuery, IEnumerable<ContatoDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ObterContatosQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContatoDto>> Handle(ObterContatosQuery request, CancellationToken cancellationToken)
        {
            var contatos = await _unitOfWork.ContatoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ContatoDto>>(contatos);
        }
    }
}