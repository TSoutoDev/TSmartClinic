using TSmartClinic.API.DTOs.Requests.Base;
using TSmartClinic.API.DTOs.Responses;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Exceptions;
using TSmartClinic.Core.Domain.Helpers.FilterHelper;
using TSmartClinic.Core.Domain.Interfaces.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TSmartClinic.API.Controllers
{
    [ApiController]
    public abstract class BaseController<TEntity, TService, TFiltro,TInsertRequestDTO, TUpdateRequestDTO,TResponseDTO> : ControllerBase
    where TEntity : Base
        where TService : IBaseService<TEntity>
        where TFiltro : BaseFiltro
        where TInsertRequestDTO : BaseRequestDTO
        where TUpdateRequestDTO : BaseRequestDTO
        where TResponseDTO : BaseResponseDTO

    {
        private readonly TService _baseService;
        private readonly IMapper _mapper;

        protected TService Service => _baseService;
        protected IMapper Mapper => _mapper;

        protected BaseController(TService baseService, IMapper mapper)
        {
            _baseService = baseService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public virtual ActionResult<TResponseDTO> ObterPorId(int id)
        {
            var obj = _baseService?.ObterPorId(id);

            if (obj == null) throw new NotFoundException();

            return StatusCode(200, _mapper.Map<TResponseDTO>(obj));
        }
        [HttpPost("listar")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public virtual ActionResult<ResponseDTO<TResponseDTO>> Listar(TFiltro filtro)
        {
            var lista = _baseService?.Listar(filtro);

            if (lista == null || lista.Count() == 0) throw new NotFoundException();

            var map = Mapper?.Map<List<TResponseDTO>>(lista);

            return StatusCode(200, new ResponseDTO<TResponseDTO>()
            {
                QuantidadeRegistros = map.Count(),
                PaginaAtual = filtro.PaginaAtual,
                Itens = map
            });
        }
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public virtual ActionResult<TResponseDTO> Inserir(TInsertRequestDTO objRequest)
        {
            var obj = Mapper?.Map<TEntity>(objRequest);

            _baseService?.Inserir(obj);

            return StatusCode(201, Mapper?.Map<TResponseDTO>(obj));
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public virtual ActionResult<TResponseDTO> Atualizar(int id, TUpdateRequestDTO objRequest)
        {
            var obj = Mapper.Map<TEntity>(objRequest);
            var objAlterado = _baseService?.Atualizar(id, obj);

            return StatusCode(200, Mapper?.Map<TResponseDTO>(objAlterado));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public virtual ActionResult Excluir(int id)
        {
            _baseService?.Excluir(id);

            return StatusCode(200);
        }

    }
}
