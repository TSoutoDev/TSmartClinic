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
            CreateMap<Cliente, BaseClienteRequestDTO>().ReverseMap();
            CreateMap<Modulo, BaseModuloRequestDTO>().ReverseMap();
            CreateMap<Nicho, BaseNichoRequestDTO>().ReverseMap();
            CreateMap<Paciente, BasePacienteRequestDTO>().ReverseMap();
            CreateMap<Perfil, BasePerfilRequestDTO>().ReverseMap();
         //   CreateMap<OperacaoPerfil, BaseOperacaoPerfilRequestDTO>().ReverseMap();

            //insert
            CreateMap<Categoria, CategoriaInsertRequestDTO>().ReverseMap();
            CreateMap<Paciente, PacienteInsertRequestDTO>().ReverseMap();
            CreateMap<Perfil, PerfilUpdateRequestDTO>().ReverseMap();
            CreateMap<Tarefa, TarefaInsertRequestDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioInsertRequestDTO>().ReverseMap();

            //Update
            CreateMap<Categoria, CategoriaUpdateRequestDTO>().ReverseMap();
            CreateMap<Modulo, ModuloUpdateRequestDTO>().ReverseMap();
            CreateMap<Paciente, PacienteUpdateRequestDTO>().ReverseMap();
            CreateMap<Tarefa, TarefaUpdateRequestDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioUpdateRequestDTO>().ReverseMap();

            //Response
            CreateMap<Cliente, ClienteResponseDTO>();
            CreateMap<Funcionalidade, FuncionalidadeResponseDTO>();
            CreateMap<Modulo, ModuloResponseDTO>();
            CreateMap<Nicho, NichoResponseDTO>();
            CreateMap<Operacao, OperacaoResponseDTO>();
            CreateMap<Categoria, CategoriaResponseDTO>().ReverseMap();
            CreateMap<Tarefa, TarefaResponseDTO>()
               .ForMember(dest => dest.CategoriaId, opt => opt.MapFrom(src => src.CategoriaId))
               .ReverseMap();
            CreateMap<Usuario, UsuarioResponseDTO>();
            CreateMap<Paciente, PacienteResponseDTO>();
            CreateMap<Perfil, PerfilResponseDTO>();
            //CreateMap<OperacaoPerfil, OperacaoPerfilResponseDTO>()
            //  .ForMember(dest => dest.PerfilId, opt => opt.MapFrom(src => src.Id))
             // .ReverseMap();
        }
    }
}
