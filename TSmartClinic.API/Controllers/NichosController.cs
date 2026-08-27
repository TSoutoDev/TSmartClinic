using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TSmartClinic.API.Controllers;
using TSmartClinic.API.Handles;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Exceptions;
using TSmartClinic.Core.Domain.Helpers.FilterHelper;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Shared.DTOs.Requests.Base;
using TSmartClinic.Shared.DTOs.Requests.Update;
using TSmartClinic.Shared.DTOs.Responses;

[Route("api/[controller]")]
[ApiController]
[PermissionModule("Nichos")]
public class NichosController : BaseController<Nicho, IBaseService<Nicho>, BaseFiltro, BaseNichoRequestDTO, NichoUpdateRequestDTO, NichoResponseDTO>
{
    private readonly INichoService _nichoService;

    public NichosController(INichoService nichoService, IMapper mapper) : base(nichoService, mapper)
    {
        _nichoService = nichoService;
    }

    [HttpGet("obter")]
    public async Task<ActionResult<List<NichoResponseDTO>>> Obter()
    {
        var lista = await _nichoService.ListarNichos();

        if (lista == null || !lista.Any())
            throw new NotFoundException();

        return StatusCode(200, Mapper.Map<List<NichoResponseDTO>>(lista));
    }
}