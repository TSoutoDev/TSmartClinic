using AgendaApp.API.DTOs.Requests.Insert;
using AgendaApp.API.DTOs.Requests.Update;
using AgendaApp.API.DTOs.Responses;
using AgendaApp.Core.Domain.Entities;
using AgendaApp.Data.Entities;
using AutoMapper;

namespace AgendaApp.API.Mapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Categoria, CategoriaInsertRequestDTO>().ReverseMap();
            CreateMap<Categoria, CategoriaUpdateRequestDTO>().ReverseMap();
            CreateMap<Categoria, CategoriaResponseDTO>().ReverseMap();
            CreateMap<Tarefa, TarefaInsertRequestDTO>().ReverseMap();
            CreateMap<Tarefa, TarefaUpdateRequestDTO>().ReverseMap();
            CreateMap<Tarefa, TarefaResponseDTO>()
                .ForMember(dest => dest.CategoriaId, opt => opt.MapFrom(src => src.CategoriaId))
                .ReverseMap();
            CreateMap<Usuario, UsuarioInsertRequestDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioUpdateRequestDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioResponseDTO>();

            //CreateMap<Servico, ServicoRequestDto>().ReverseMap();
            //CreateMap<Servico, ServicoResponseDto>().ReverseMap();
        }
    }
}
