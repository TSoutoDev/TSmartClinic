using Microsoft.AspNetCore.Mvc;
using TSmartClinic.Core.Domain.Interfaces.Services;

namespace TSmartClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunicipiosController : ControllerBase
    {
        private readonly IMunicipioService _municipioService;

        public MunicipiosController(IMunicipioService municipioService)
        {
            _municipioService = municipioService;
        }

        [HttpGet("ibge/{id}")]
        public async Task<IActionResult> ObterPorIbge(int id)
        {
            var municipio = await _municipioService.ObterPorId(id);

            if (municipio == null)
                return NotFound();

            return Ok(new
            {
                municipioId = municipio.Id,
                nomeMunicipio = municipio.NomeMunicipio,
                estadoId = municipio.Codigo_uf
            });
        }
    }
}