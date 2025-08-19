using TSmartClinic.Shared.DTOs.Requests.Insert;
using TSmartClinic.Shared.DTOs.Requests.Update;
using TSmartClinic.Shared.DTOs.Responses;
using TSmartClinic.Shared.DTOs.Responses.PermissoesAcessoRersponse;
using TSmartClinic.Shared.DTOs.Requests.Base;
//using TSmartClinic.API.DTOs.Requests.Base;
//using TSmartClinic.API.DTOs.Responses.PermissoesAcessoResponse;
//using TSmartClinic.API.DTOs.Requests.Insert;
//using TSmartClinic.API.DTOs.Requests.Update;
//using TSmartClinic.API.DTOs.Responses;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Data.Entities;
using AutoMapper;




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
            CreateMap<OperacaoPerfil, BaseOperacaoPerfilRequestDTO>().ReverseMap();
             CreateMap<Perfil, BasePerfilRequestDTO>().ReverseMap();

            //insert
            CreateMap<Categoria, CategoriaInsertRequestDTO>().ReverseMap();
            CreateMap<Paciente, PacienteInsertRequestDTO>().ReverseMap();
            CreateMap<Tarefa, TarefaInsertRequestDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioInsertRequestDTO>().ReverseMap();

            //Update
            CreateMap<Categoria, CategoriaUpdateRequestDTO>().ReverseMap();
            CreateMap<Modulo, ModuloUpdateRequestDTO>().ReverseMap();
            CreateMap<Paciente, PacienteUpdateRequestDTO>().ReverseMap();
            CreateMap<Perfil, PerfilUpdateRequestDTO>()
             .ForMember(dest => dest.OperacaoPerfis, opt => opt.MapFrom(src => src.OperacaoPerfis)).ReverseMap();

            CreateMap<Tarefa, TarefaUpdateRequestDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioUpdateRequestDTO>().ReverseMap();
            CreateMap<OperacaoPerfil, UsuarioUpdateRequestDTO>().ReverseMap();

            //Response
            CreateMap<Cliente, ClienteResponseDTO>();
            CreateMap<Nicho, NichoResponseDTO>();
            CreateMap<Categoria, CategoriaResponseDTO>().ReverseMap();
            CreateMap<Tarefa, TarefaResponseDTO>()
               .ForMember(dest => dest.CategoriaId, opt => opt.MapFrom(src => src.CategoriaId))
               .ReverseMap();
            CreateMap<Usuario, UsuarioResponseDTO>();
            CreateMap<Paciente, PacienteResponseDTO>();
            CreateMap<Perfil, PerfilResponseDTO>()
                 .ForMember(dest => dest.OperacaoPerfis, opt => opt.MapFrom(src => src.OperacaoPerfis))
                 .ReverseMap();
            
            CreateMap<OperacaoPerfil, OperacaoPerfilResponseDTO>().ReverseMap();
            CreateMap<Operacao, PermissoesAcessoResponseDTO.OperacaoResponseDTO>().ReverseMap();
            CreateMap<Funcionalidade, PermissoesAcessoResponseDTO.FuncionalidadeResponseDTO>().ReverseMap();
            CreateMap<Modulo, PermissoesAcessoResponseDTO.ModuloResponseDTO>().ReverseMap();



        }
    }
}
