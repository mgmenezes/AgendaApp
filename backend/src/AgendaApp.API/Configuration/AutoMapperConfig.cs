// AgendaApp.API/Configuration/AutoMapperConfig.cs
using AutoMapper;
using AgendaApp.Domain.Entities;
using AgendaApp.Application.DTOs;

namespace AgendaApp.API.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            // Mapeamento de Entidade para DTO
            CreateMap<Contato, ContatoDto>();

            // Mapeamento de DTO para Entidade
            CreateMap<CriarContatoDto, Contato>();
            CreateMap<AtualizarContatoDto, Contato>();
        }
    }
}