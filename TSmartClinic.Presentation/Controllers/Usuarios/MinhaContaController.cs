using Microsoft.AspNetCore.Mvc;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.Services.Interfaces;

namespace TSmartClinic.Presentation.Controllers.Usuarios
{
    public class MinhaContaController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public MinhaContaController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<IActionResult> DetalhesMinhaConta()
        {
            var response = await _usuarioService.ObterMinhaConta();

            if (response == null || !response.Sucesso || response.Itens == null || !response.Itens.Any())
            {
                return Json(new
                {
                    sucesso = false,
                    mensagem = "Usuário não encontrado."
                });
            }

            var usuario = response.Itens.First();

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
