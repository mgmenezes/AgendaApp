using AutoMapper;
using FluentValidation;
using AgendaApp.Domain.Entities;
using AgendaApp.Domain.Interfaces;
using AgendaApp.Application.DTOs;
using AgendaApp.Application.Interfaces;
using AgendaApp.Application.MessageBus.Interfaces;
namespace AgendaApp.Application.Services
{
    public class ContatoService : IContatoService

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CriarContatoDto> _criarValidator;
        // private readonly IValidator<AtualizarContatoDto> _atualizarValidator;
        private readonly IMessageBusService _messageBusService;

        public ContatoService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IValidator<CriarContatoDto> criarValidator,
            IMessageBusService messageBusService
            // IValidator<AtualizarContatoDto> atualizarValidator
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _criarValidator = criarValidator;
            _messageBusService = messageBusService;
            // _atualizarValidator = atualizarValidator;
        }

        public async Task<ContatoDto> ObterPorIdAsync(Guid id)
        {
            var contato = await _unitOfWork.ContatoRepository.ObterPorIdAsync(id);
            if (contato == null)
                throw new NotFoundException($"Contato com Id {id} não encontrado");

            return _mapper.Map<ContatoDto>(contato);
        }

        public async Task<IEnumerable<ContatoDto>> ObterTodosAsync()
        {
            var contatos = await _unitOfWork.ContatoRepository.ObterTodosAsync();
            return _mapper.Map<IEnumerable<ContatoDto>>(contatos);
        }

        public async Task<ContatoDto> CriarAsync(CriarContatoDto dto)
        {

            await _criarValidator.ValidateAndThrowAsync(dto);


            if (await _unitOfWork.ContatoRepository.ExisteEmailAsync(dto.Email))
                throw new ValidationException("Email já cadastrado");


            var contato = _mapper.Map<Contato>(dto);
            await _unitOfWork.ContatoRepository.AdicionarAsync(contato);
            await _unitOfWork.CommitAsync();


            _messageBusService.PublishMessage("contato-queue", contato);

            return _mapper.Map<ContatoDto>(contato);
        }

        public async Task<ContatoDto> AtualizarAsync(AtualizarContatoDto dto)
        {
            var contato = await _unitOfWork.ContatoRepository.ObterPorIdAsync(dto.Id);
            if (contato == null)
                throw new NotFoundException($"Contato com Id {dto.Id} não encontrado");


            var contatoExistente = await _unitOfWork.ContatoRepository.ObterPorEmailAsync(dto.Email);
            if (contatoExistente != null && contatoExistente.Id != dto.Id)
                throw new ValidationException("Email já está em uso por outro contato");


            var contatoAtualizado = _mapper.Map(dto, contato);
            contatoAtualizado.DataAtualizacao = DateTime.UtcNow;
            await _unitOfWork.ContatoRepository.AtualizarAsync(contatoAtualizado);
            await _unitOfWork.CommitAsync();

            // Publicação da mensagem no RabbitMQ
            _messageBusService.PublishMessage("contato-queue", contatoAtualizado);

            return _mapper.Map<ContatoDto>(contatoAtualizado);
        }

        public async Task InativarAsync(Guid id)
        {
            var contato = await _unitOfWork.ContatoRepository.ObterPorIdAsync(id);
            if (contato == null)
                throw new NotFoundException($"Contato com Id {id} não encontrado");
            contato.DataAtualizacao = DateTime.UtcNow;
            await _unitOfWork.ContatoRepository.InativarAsync(id);
            await _unitOfWork.CommitAsync();


            _messageBusService.PublishMessage("contato-queue", contato);
        }
    }


    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}