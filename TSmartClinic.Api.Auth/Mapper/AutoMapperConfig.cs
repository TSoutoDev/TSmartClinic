using AutoMapper;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Models;

namespace TSmartClinic.Api.Auth.Mapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<AutenticacaoModel, Usuario>().ReverseMap();
        }
    }
}
