using Microsoft.AspNetCore.Authorization;
using TSmartClinic.API.Handles;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Services;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IPermissaoCacheService _permissaoCacheService;
    private readonly IUsuarioService _usuarioService;
    private readonly IUsuarioUnidadePerfilService _usuarioUnidadePerfilService;

    public PermissionAuthorizationHandler(IPermissaoCacheService permissaoCacheService, IUsuarioService usuarioService, IUsuarioUnidadePerfilService usuarioUnidadePerfilService)
    {
        _permissaoCacheService = permissaoCacheService;
        _usuarioService = usuarioService;
        _usuarioUnidadePerfilService = usuarioUnidadePerfilService;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var tipoUsuario = context.User.FindFirst("TipoUsuario")?.Value;

        // Master tem acesso total
        if (string.Equals(tipoUsuario,"M", StringComparison.OrdinalIgnoreCase))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        var usuarioIdClaim = context.User.FindFirst("Usuario_Id")?.Value;
        var unidadeIdClaim = context.User.FindFirst("Unidade_Id")?.Value;

        if (!int.TryParse(usuarioIdClaim, out var usuarioId))
            return Task.CompletedTask;

        if (!int.TryParse(unidadeIdClaim, out var unidadeId))
            return Task.CompletedTask;

        // tenta buscar no cache
        var permissoes = _permissaoCacheService.ObterPermissoes(usuarioId, unidadeId);

        // se não encontrou, busca no banco
        if (permissoes == null)
        {
            var perfilId = _usuarioUnidadePerfilService.ObterPerfilIdPorUsuarioUnidade(usuarioId, unidadeId);

            if (!perfilId.HasValue)
                return Task.CompletedTask;

            permissoes = _usuarioService.ObterPermissoesPorPerfil(perfilId.Value);

            // salva no cache
            _permissaoCacheService.SalvarPermissoes(usuarioId, unidadeId, permissoes);
        }

        //valida a permissão
        if (permissoes.Contains(requirement.Permissao, StringComparer.OrdinalIgnoreCase))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}