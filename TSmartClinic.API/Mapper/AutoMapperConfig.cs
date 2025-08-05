using TSmartClinic.API.DTOs.Requests.Insert;
using TSmartClinic.API.DTOs.Requests.Update;
using TSmartClinic.API.DTOs.Responses;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Data.Entities;
using AutoMapper;
using TSmartClinic.API.DTOs.Requests.Base;

namespace AgendaApp.API.Mapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {

            //Base
            CreateMap<Paciente, BasePacienteRequestDTO>().ReverseMap();
            CreateMap<Nicho, BaseNichoRequestDTO>().ReverseMap();
            CreateMap<Perfil, BasePerfilRequestDTO>().ReverseMap();

            //insert
            CreateMap<Categoria, CategoriaInsertRequestDTO>().ReverseMap();
            CreateMap<Tarefa, TarefaInsertRequestDTO>().ReverseMap();         
            CreateMap<Usuario, UsuarioInsertRequestDTO>().ReverseMap();
            CreateMap<Paciente, PacienteInsertRequestDTO>().ReverseMap();

            //Update
            CreateMap<Categoria, CategoriaUpdateRequestDTO>().ReverseMap();
            CreateMap<Tarefa, TarefaUpdateRequestDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioUpdateRequestDTO>().ReverseMap();
            CreateMap<Paciente, PacienteUpdateRequestDTO>().ReverseMap();

            //Response
            CreateMap<Convenio, ConvenioResponseDTO>().ReverseMap(); 
            CreateMap<Categoria, CategoriaResponseDTO>().ReverseMap();
            CreateMap<Tarefa, TarefaResponseDTO>()
               .ForMember(dest => dest.CategoriaId, opt => opt.MapFrom(src => src.CategoriaId))
               .ReverseMap();
            CreateMap<Usuario, UsuarioResponseDTO>();
            CreateMap<Paciente, PacienteResponseDTO>();
            CreateMap<Nicho, NichoResponseDTO>();
            CreateMap<Perfil, PerfilResponseDTO>();
        }
    }
}
