using Microsoft.AspNetCore.Authorization;
using TSmartClinic.API.Handles;
using TSmartClinic.Core.Domain.Interfaces.Services;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IPermissaoCacheService _permissaoCacheService;
    private readonly IUsuarioService _usuarioService;
    private readonly IUsuarioClientePerfilService _usuarioClientePerfilService;

    public PermissionAuthorizationHandler(IPermissaoCacheService permissaoCacheService, IUsuarioService usuarioService, IUsuarioClientePerfilService usuarioClientePerfilService)
    {
        _permissaoCacheService = permissaoCacheService;
        _usuarioService = usuarioService;
        _usuarioClientePerfilService = usuarioClientePerfilService;
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

        if (!int.TryParse(usuarioIdClaim, out var usuarioId))
            return Task.CompletedTask;

        // tenta buscar no cache
        var permissoes =  _permissaoCacheService.ObterPermissoes(usuarioId);

        // se não encontrou, busca no banco
        if (permissoes == null)
        {
            var clinicas = _usuarioClientePerfilService.ObterClinicasDoUsuario(usuarioId);

            permissoes = _usuarioService.ObterPermissaoUsuario(usuarioId, clinicas);

            // salva no cache
            _permissaoCacheService.SalvarPermissoes(usuarioId, permissoes);
        }

        //valida a permissão
        if (permissoes.Contains(requirement.Permissao, StringComparer.OrdinalIgnoreCase))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}