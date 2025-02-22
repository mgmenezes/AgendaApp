// AgendaApp.Application/Mappings/MappingProfile.cs
using AutoMapper;
using AgendaApp.Domain.Entities;
using AgendaApp.Application.DTOs;

namespace AgendaApp.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeamento de Contato para ContatoDto
            CreateMap<Contato, ContatoDto>();

            // Mapeamento de CriarContatoDto para Contato
            CreateMap<CriarContatoDto, Contato>()
                .ConstructUsing(dto => new Contato(dto.Nome, dto.Email, dto.Telefone));
        }
    }
}