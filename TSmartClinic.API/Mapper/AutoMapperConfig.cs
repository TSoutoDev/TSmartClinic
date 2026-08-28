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
            CreateMap<Modulo, BaseModuloRequestDTO>().ReverseMap();
            CreateMap<Nicho, BaseNichoRequestDTO>().ReverseMap();
            CreateMap<Paciente, BasePacienteRequestDTO>().ReverseMap();
            CreateMap<OperacaoPerfil, BaseOperacaoPerfilRequestDTO>().ReverseMap();
            CreateMap<Perfil, BasePerfilRequestDTO>().ReverseMap();
            CreateMap<UsuarioClientePerfil, BaseUsuarioClientePerfilRequestDto>().ReverseMap();
            CreateMap<Convenio, BaseConvenioRequestDTO>().ReverseMap();
            CreateMap<BaseEnderecoRequestDTO, Endereco>().ReverseMap();
            CreateMap<BaseClienteRequestDTO, Cliente>()
            .ForMember(dest => dest.ClienteEndereco,
                opt => opt.MapFrom(src => src.ClienteEnderecos));
            CreateMap<BaseClienteEnderecoRequestDTO, ClienteEndereco>().ReverseMap();

            //insert
            CreateMap<Categoria, CategoriaInsertRequestDTO>().ReverseMap();
            CreateMap<Paciente, PacienteInsertRequestDTO>().ReverseMap();
            CreateMap<Tarefa, TarefaInsertRequestDTO>().ReverseMap();
            CreateMap<PacienteEnderecoRequestDTO, PacienteEndereco>().ReverseMap();
            CreateMap<UsuarioInsertRequestDTO, Usuario>()
                .ForMember(d => d.DataInclusao,
                    opt => opt.MapFrom(src => src.DataInclusao ?? DateTime.UtcNow))
                .ReverseMap();
            CreateMap<UsuarioClientePerfil, UsuarioClientePerfilInsertRequestDto>()
                .ReverseMap();

            //Update
            CreateMap<Categoria, CategoriaUpdateRequestDTO>().ReverseMap();
            CreateMap<Modulo, ModuloUpdateRequestDTO>().ReverseMap();
            CreateMap<Paciente, PacienteUpdateRequestDTO>().ReverseMap();
            CreateMap<PerfilUpdateRequestDTO, Perfil>()
             .ForMember(dest => dest.Id, opt => opt.Ignore())
             .ForMember(dest => dest.PublicId, opt => opt.Ignore())
             .ForMember(dest => dest.OperacaoPerfis,
                 opt => opt.MapFrom(src => src.OperacaoPerfis));

            CreateMap<Perfil, PerfilUpdateRequestDTO>()
                .ForMember(dest => dest.OperacaoPerfis,
                    opt => opt.MapFrom(src => src.OperacaoPerfis));

            CreateMap<Tarefa, TarefaUpdateRequestDTO>().ReverseMap();
            CreateMap<PacienteUpdateRequestDTO, Paciente>().ReverseMap();
            CreateMap<NichoUpdateRequestDTO, Nicho>().ReverseMap();

            CreateMap<UsuarioUpdateRequestDTO, Usuario>()
            .ForMember(dest => dest.PublicId, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UsuarioClientePerfil,
                opt => opt.MapFrom(src => src.UsuarioClientePerfil));

            CreateMap<Usuario, UsuarioUpdateRequestDTO>()
                .ForMember(dest => dest.UsuarioClientePerfil,
                    opt => opt.MapFrom(src => src.UsuarioClientePerfil));

            CreateMap<UsuarioClientePerfilUpdateRequestDto, UsuarioClientePerfil>()
                .ForMember(dest => dest.UsuarioId, opt => opt.Ignore());

            CreateMap<UsuarioClientePerfil, UsuarioClientePerfilUpdateRequestDto>();
            CreateMap<ConvenioUpdateRequestDTO, Convenio>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PublicId, opt => opt.Ignore());



            //Response
            CreateMap<Nicho, NichoResponseDTO>();
            CreateMap<Categoria, CategoriaResponseDTO>().ReverseMap();
            CreateMap<Convenio, ConvenioResponseDTO>().ReverseMap(); ;
            CreateMap<Paciente, PacienteResponseDTO>().ReverseMap();

            CreateMap<Tarefa, TarefaResponseDTO>()
               .ForMember(dest => dest.CategoriaId, opt => opt.MapFrom(src => src.CategoriaId))
               .ReverseMap();
            
            CreateMap<Perfil, PerfilResponseDTO>()
                 .ForMember(dest => dest.OperacaoPerfis, opt => opt.MapFrom(src => src.OperacaoPerfis))
                 .ReverseMap();

            CreateMap<OperacaoPerfil, OperacaoPerfilResponseDTO>().ReverseMap();
            CreateMap<Operacao, PermissoesAcessoResponseDTO.OperacaoResponseDTO>().ReverseMap();
            CreateMap<Funcionalidade, PermissoesAcessoResponseDTO.FuncionalidadeResponseDTO>().ReverseMap();
            CreateMap<Modulo, PermissoesAcessoResponseDTO.ModuloResponseDTO>().ReverseMap();
            CreateMap<Endereco, EnderecoResponseDTO>().ReverseMap();
            CreateMap<PacienteEndereco, PacienteEnderecoResponseDTO>().ReverseMap();
            CreateMap<Paciente, PacienteResponseDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioResponseDTO>().ReverseMap();

            CreateMap<UsuarioClientePerfil, UsuarioClientePerfilResponseDto>()
                .ForMember(dest => dest.Perfil,
                    opt => opt.MapFrom(src => src.Perfil))
                .ReverseMap();

            CreateMap<Cliente, ClienteResponseDTO>()
                .ForMember(dest => dest.ClienteEnderecos,
                    opt => opt.MapFrom(src => src.ClienteEndereco))
                .ReverseMap();

            CreateMap<ClienteEndereco, ClienteEnderecoResponseDTO>().ReverseMap();
        }
    }
}
