// AgendaApp.Application/Interfaces/IContatoService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgendaApp.Application.DTOs;  // Adicionando a referência aos DTOs

namespace AgendaApp.Application.Interfaces
{
    public interface IContatoService
    {
        Task<ContatoDto> ObterPorIdAsync(Guid id);
        Task<IEnumerable<ContatoDto>> ObterTodosAsync();
        Task<ContatoDto> CriarAsync(CriarContatoDto dto);
        Task<ContatoDto> AtualizarAsync(AtualizarContatoDto dto);
        Task InativarAsync(Guid id);
    }
}