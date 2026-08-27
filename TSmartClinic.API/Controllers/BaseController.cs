using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TSmartClinic.API.Handles;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Exceptions;
using TSmartClinic.Core.Domain.Helpers.FilterHelper;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Shared.DTOs.Requests.Base;
using TSmartClinic.Shared.DTOs.Responses;

namespace TSmartClinic.API.Controllers
{
    [ApiController]
    public abstract class BaseController<TEntity, TService, TFiltro, TInsertRequestDTO, TUpdateRequestDTO, TResponseDTO> : ControllerBase
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

        [HttpGet("{publicId:guid}")]
        [AuthorizePermission("Acessar")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public virtual ActionResult<TResponseDTO> ObterPorPublicId(Guid publicId)
        {
            var obj = _baseService.ObterPorPublicId(publicId);

            if (obj == null)
                throw new NotFoundException();

            return StatusCode(200, _mapper.Map<TResponseDTO>(obj));
        }

        [HttpPost("listar")]
        [AuthorizePermission("Acessar")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public virtual ActionResult<ResponseDTO<TResponseDTO>> Listar(TFiltro filtro)
        {
            var lista = _baseService.Listar(filtro);

            if (lista == null || !lista.Any())
                throw new NotFoundException();

            var map = _mapper.Map<List<TResponseDTO>>(lista);

            return StatusCode(200, new ResponseDTO<TResponseDTO>
            {
                QuantidadeRegistros = map.Count,
                PaginaAtual = filtro.PaginaAtual,
                Itens = map
            });
        }

        [HttpPost]
        [AuthorizePermission("Incluir")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public virtual ActionResult<TResponseDTO> Inserir(TInsertRequestDTO objRequest)
        {
            var obj = _mapper.Map<TEntity>(objRequest);

            var objInserido = _baseService.Inserir(obj);

            return StatusCode(201, _mapper.Map<TResponseDTO>(objInserido));
        }

        [HttpPatch("{publicId:guid}")]
        [AuthorizePermission("Editar")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public virtual ActionResult<TResponseDTO> Atualizar(Guid publicId, TUpdateRequestDTO objRequest)
        {
            var obj = _mapper.Map<TEntity>(objRequest);

            var objAlterado = _baseService.Atualizar(publicId, obj);

            return StatusCode(200, _mapper.Map<TResponseDTO>(objAlterado));
        }

        [HttpDelete("{publicId:guid}")]
        [AuthorizePermission("Excluir")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public virtual ActionResult Excluir(Guid publicId)
        {
            _baseService.Excluir(publicId);

            return StatusCode(200);
        }
    }
}