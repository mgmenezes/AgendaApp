using AutoMapper;
using AgendaApp.Domain.Entities;
using AgendaApp.Application.DTOs;
using AgendaApp.Application.Commands.Contatos.AtualizarContato;

namespace AgendaApp.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Contato, ContatoDto>();

            CreateMap<CriarContatoDto, Contato>();

            CreateMap<AtualizarContatoDto, Contato>();

            CreateMap<AtualizarContatoCommand, Contato>();
        }
    }
}