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
            CreateMap<AutenticacaoModel, Cliente>().ReverseMap();
          
        }
    }
}
