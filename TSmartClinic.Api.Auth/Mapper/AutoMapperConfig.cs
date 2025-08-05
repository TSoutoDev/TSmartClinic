using AutoMapper;
using TSmartClinic.Api.Auth.DTOs;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Models;
using TSmartClinic.Data.Entities;

namespace TSmartClinic.Api.Auth.Mapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<AutenticacaoModel, Usuario>().ReverseMap();
            CreateMap<UsuarioClientePerfil,UsuarioClinicaPerfilRequestDto>().ReverseMap();
            CreateMap<UsuarioClientePerfil, UsuarioClinicaPerfilResponseDto>()
                 .ForMember(dest => dest.UsuarioId, opt => opt.MapFrom(src => src.Id)).ReverseMap();



        }
    }
}
