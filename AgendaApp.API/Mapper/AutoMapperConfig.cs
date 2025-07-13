using AgendaApp.API.DTOs.Requests.Insert;
using AgendaApp.API.DTOs.Requests.Update;
using AgendaApp.API.DTOs.Responses;
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
            //CreateMap<Servico, ServicoRequestDto>().ReverseMap();
            //CreateMap<Servico, ServicoResponseDto>().ReverseMap();
        }
    }
}
