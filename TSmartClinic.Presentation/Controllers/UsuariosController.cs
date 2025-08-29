using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.Services.Interfaces;
using TSmartClinic.Presentation.ViewModels.Filters;

namespace TSmartClinic.Presentation.Controllers
{
    public class UsuariosController : BaseController<IUsuarioService, UsuarioFilterViewModel, UsuarioViewModel>
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IPerfilService _perfilService;
        private readonly IUsuarioLogadoService _usuarioLogadoService;

        public UsuariosController(IUsuarioLogadoService usuarioLogadoService, IPerfilService perfilService, IUsuarioService usuarioService) : base(usuarioService)
        {
            _usuarioService = usuarioService;
            _perfilService = perfilService;
            _usuarioLogadoService = usuarioLogadoService;
        }

        // POST: Cadastro
        public override async Task<IActionResult> Cadastro(UsuarioViewModel model)
        {
            // Validação: usuário precisa selecionar um perfil
            if (!model.PerfilClienteId.HasValue)
            {
                ModelState.AddModelError("PerfilClienteId", "Selecione um perfil válido.");
            }

            if (!ModelState.IsValid)
            {
                // Recria dropdown caso haja erro
                await CriarViewPerfisPorCliente(model.ClienteId);
                return View(model);
            }

            // Processa a foto e preenche dados
            var foto = Request.Form["Foto"].ToString();
            await _usuarioService.ProcessarFotoAsync(model, foto);
            await _usuarioService.PreencherDados(model);
            
            
            model.ClienteId = _usuarioLogadoService.ClienteId ?? 1; // pega da sessão ou fallback Cliente Master

            var ucp = new UsuarioClientePerfilViewModel
            {
                ClienteId = _usuarioLogadoService.ClienteId.Value,
                PerfilId = model.PerfilClienteId.Value,
                ClientePadrao = false
            };

            model.UsuarioClientePerfil = new List<UsuarioClientePerfilViewModel> { ucp };

            return await base.Cadastro(model);
        }

        // GET: Cadastro
        public override async Task<IActionResult> Cadastro(int? id)
        {
            await CriarViewBags();

            var result = await base.Cadastro(id) as ViewResult;

            if (result?.Model is UsuarioViewModel model)
            {
                // CADASTRO NOVO
                if (!id.HasValue)
                {
                    model.DataExpiracaoSenha = DateTime.Today.AddDays(365);
                }

                // Obtem cliente do claim
                var tipoUsuario = User.FindFirst("Usuario_Tipo")?.Value;
                var clienteIdClaim = User.FindFirst("Cliente_Id")?.Value;
                int clienteId = model.ClienteId;

                if (!string.IsNullOrEmpty(tipoUsuario) && tipoUsuario != "M" && !string.IsNullOrEmpty(clienteIdClaim))
                {
                    clienteId = int.Parse(clienteIdClaim); 
                }

                if (model.UsuarioClientePerfil != null && model.UsuarioClientePerfil.Any())
                {
                    model.PerfilClienteId = model.UsuarioClientePerfil.First().PerfilId;
                }

                await CriarViewPerfisPorCliente(clienteId, model.PerfilClienteId);
            }

            return result;
        }

        // GET: Busca Avançada
        public override async Task<IActionResult> BuscaAvancada(UsuarioFilterViewModel filtro)
        {
            return await base.BuscaAvancada(filtro);
        }

        // GET: Consulta
        public override async Task<IActionResult> Consulta()
        {
            return await base.Consulta();
        }

        private async Task CriarViewBags()
        {
            // Aqui você pode popular outros ViewBags se necessário
        }

        private async Task CriarViewPerfisPorCliente(int clienteId, int? perfilSelecionado = null)
        {
            var resultado = await _perfilService.ListarPerfilPorCliente(clienteId);

            var lista = resultado
                .Select(x => new SelectListItem
                {
                    Text = x.NomePerfil,
                    Value = x.Id.ToString()
                })
                .ToList();

            // Insere opção inicial (placeholder) sem definir Selected
            lista.Insert(0, new SelectListItem { Text = "- Selecione o Perfil -", Value = "" });

            ViewBag.Perfis = lista;
        }
    }
}
