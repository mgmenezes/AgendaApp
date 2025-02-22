// AgendaApp.Application/Services/ContatoService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using AgendaApp.Domain.Entities;
using AgendaApp.Domain.Interfaces;
using AgendaApp.Application.DTOs;
using AgendaApp.Application.Interfaces;

namespace AgendaApp.Application.Services
{
    public class ContatoService : IContatoService
    {
        private readonly IUnitOfWork _unitOfWork;
                private readonly IMapper _mapper;
        private readonly IValidator<CriarContatoDto> _criarValidator;
        // private readonly IValidator<AtualizarContatoDto> _atualizarValidator;

        public ContatoService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IValidator<CriarContatoDto> criarValidator
            // IValidator<AtualizarContatoDto> atualizarValidator
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _criarValidator = criarValidator;
            // _atualizarValidator = atualizarValidator;
        }

        // Implementação dos métodos da interface
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
            // Validação dos dados de entrada
            await _criarValidator.ValidateAndThrowAsync(dto);

            // Verificação de email duplicado
            if (await _unitOfWork.ContatoRepository.ExisteEmailAsync(dto.Email))
                throw new ValidationException("Email já cadastrado");

            // Criação do novo contato
            var contato = _mapper.Map<Contato>(dto);
            await _unitOfWork.ContatoRepository.AdicionarAsync(contato);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<ContatoDto>(contato);
        }

        public async Task<ContatoDto> AtualizarAsync(AtualizarContatoDto dto)
        {
            var contato = await _unitOfWork.ContatoRepository.ObterPorIdAsync(dto.Id);
            if (contato == null)
                throw new NotFoundException($"Contato com Id {dto.Id} não encontrado");

            // Verifica se o novo email já existe para outro contato
            var contatoExistente = await _unitOfWork.ContatoRepository.ObterPorEmailAsync(dto.Email);
            if (contatoExistente != null && contatoExistente.Id != dto.Id)
                throw new ValidationException("Email já está em uso por outro contato");

            // Atualiza os dados
            var contatoAtualizado = _mapper.Map(dto, contato);
            contatoAtualizado.DataAtualizacao = DateTime.UtcNow;
            await _unitOfWork.ContatoRepository.AtualizarAsync(contatoAtualizado);
            await _unitOfWork.CommitAsync();

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
        }
    }

    // Classe de exceção personalizada para registros não encontrados
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}