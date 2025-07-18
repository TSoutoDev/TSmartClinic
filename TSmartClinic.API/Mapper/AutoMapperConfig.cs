using TSmartClinic.API.DTOs.Requests.Insert;
using TSmartClinic.API.DTOs.Requests.Update;
using TSmartClinic.API.DTOs.Responses;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Data.Entities;
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
