using Microsoft.AspNetCore.Mvc;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.Services.Interfaces;

namespace TSmartClinic.Presentation.Controllers.Usuarios
{
    public class MinhaContaController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IUsuarioLogadoService _usuarioLogadoService;

        public MinhaContaController(IUsuarioService usuarioService, IUsuarioLogadoService usuarioLogadoService)
        {
            _usuarioService = usuarioService;
            _usuarioLogadoService = usuarioLogadoService;
        }

        public async Task<IActionResult> DetalhesMinhaConta()
        {
            var UsuarioLogadoId = _usuarioLogadoService.UsuarioLogadoId;

            var response = await _usuarioService.ObterPorId(UsuarioLogadoId.Value);

            if (response == null || !response.Sucesso)
                return Json(new { sucesso = false, mensagem = "Usuário não encontrado." });

            var usuario = response.Itens.FirstOrDefault();

            if (usuario == null)
                return Json(new { sucesso = false, mensagem = "Usuário não encontrado." });

            var viewModel = new MinhaContaViewModel
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Celular = usuario.Celular,
                Foto = usuario.Foto,
                NomePerfil = usuario.UsuarioClientePerfil?
                 .Select(up => up.Perfil.NomePerfil)
                 .FirstOrDefault() 
            };

            return PartialView("_MinhaContaPartial", viewModel);
        }

    }
}
