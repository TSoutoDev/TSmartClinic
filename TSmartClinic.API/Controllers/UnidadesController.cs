using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TSmartClinic.API.Handles;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Helpers.FilterHelper;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Shared.DTOs.Requests.Insert;
using TSmartClinic.Shared.DTOs.Requests.Update;
using TSmartClinic.Shared.DTOs.Responses;

namespace TSmartClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [PermissionModule("Unidades")]
    public class UnidadesController : BaseController<Unidade, IUnidadeService, BaseFiltro, UnidadeInsertRequestDTO, UnidadeUpdateRequestDTO, UnidadeResponseDTO>
    {
        private readonly IUnidadeService _unidadeService;

        public UnidadesController(IUnidadeService unidadeService, IMapper mapper) : base(unidadeService, mapper)
        {
            _unidadeService = unidadeService;
        }

        [HttpGet("dropdown-unidades/{clienteId:int}")]
        public IActionResult ListarPorCliente(int clienteId)
        {
            var unidades = _unidadeService.ListarPorCliente(clienteId);

            var resultado = unidades.Select(x => new
            {
                id = x.Id,
                publicId = x.PublicId,
                clienteId = x.ClienteId,
                nomeUnidade = x.NomeUnidade,
                unidadePrincipal = x.UnidadePrincipal
            });

            return Ok(resultado);
        }
    }
}