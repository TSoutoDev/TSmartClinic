using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.Services.Interfaces;
using TSmartClinic.Presentation.ViewModels.Filters;

namespace TSmartClinic.Presentation.Controllers.Usuarios
{
    public class UsuariosController : BaseController<IUsuarioService, UsuarioFilterViewModel, UsuarioViewModel>
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IPerfilService _perfilService;
        private readonly IUsuarioLogadoService _usuarioLogadoService;
        private readonly IClienteService _clienteService;
        private readonly IUnidadeService _unidadeService;

        public UsuariosController(IClienteService clienteService, IUsuarioLogadoService usuarioLogadoService, IPerfilService perfilService, IUsuarioService usuarioService, IUnidadeService unidadeService) : base(usuarioService)
        {
            _usuarioService = usuarioService;
            _perfilService = perfilService;
            _unidadeService = unidadeService;
            _usuarioLogadoService = usuarioLogadoService;
            _clienteService = clienteService;
        }

        // POST: Cadastro
        [HttpPost]
        public override async Task<IActionResult> Cadastro(UsuarioViewModel model)
        {
            if (!model.UnidadeId.HasValue)
            {
                ModelState.AddModelError("UnidadeId", "Selecione uma unidade válida.");
            }

            if (!model.PerfilClienteId.HasValue)
            {
                ModelState.AddModelError("PerfilClienteId", "Selecione um perfil válido.");
            }

            // Usuário não Master sempre utiliza o Cliente do contexto ativo
            if (!_usuarioLogadoService.UsuarioMaster && _usuarioLogadoService.ClienteId.HasValue)
            {
                model.ClienteId = _usuarioLogadoService.ClienteId.Value;
            }

            if (model.ClienteId > 0)
            {
                if (model.UnidadeId.HasValue)
                {
                    var unidadesCliente = await _unidadeService.ListarPorCliente(model.ClienteId);

                    if (!unidadesCliente.Any(x => x.Id == model.UnidadeId.Value))
                    {
                        ModelState.AddModelError("UnidadeId", "A unidade selecionada não pertence ao cliente informado.");
                    }
                }

                if (model.PerfilClienteId.HasValue)
                {
                    var perfisCliente = await _perfilService.ListarPerfilPorCliente(model.ClienteId);

                    if (!perfisCliente.Any(x => x.Id == model.PerfilClienteId.Value))
                    {
                        ModelState.AddModelError("PerfilClienteId", "O perfil selecionado não pertence ao cliente informado.");
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                await CriarViewBags();

                if (model.ClienteId > 0)
                {
                    await CriarViewUnidadesPorCliente(model.ClienteId, model.UnidadeId);
                    await CriarViewPerfisPorCliente(model.ClienteId, model.PerfilClienteId);
                }

                return View(model);
            }

            var foto = Request.Form["Foto"].ToString();

            await _usuarioService.ProcessarFotoAsync(model, foto);
            await _usuarioService.PreencherDados(model);

            var vinculo = new UsuarioUnidadePerfilViewModel
            {
                UnidadeId = model.UnidadeId.Value,
                PerfilId = model.PerfilClienteId.Value,
                UnidadePadrao = true
            };

            model.UsuarioUnidadePerfil = new List<UsuarioUnidadePerfilViewModel> { vinculo };

            var publicId = model.PublicId;

            var resultado = await base.Cadastro(model);

            if (publicId.HasValue && publicId.Value != Guid.Empty && TempData["MensagemErro"] == null)
                return RedirectToAction(nameof(Cadastro), new { publicId });

            return resultado;
        }

        // GET: Cadastro
        [HttpGet]
        public override async Task<IActionResult> Cadastro(Guid? publicId)
        {
            var result = await base.Cadastro(publicId) as ViewResult;

            if (result?.Model is UsuarioViewModel model)
            {
                // CADASTRO NOVO
                if (!publicId.HasValue)
                {
                    model.DataExpiracaoSenha = DateTime.Today.AddDays(365);
                }

                // EDIÇÃO - recupera Unidade e Perfil do vínculo existente
                if (model.UsuarioUnidadePerfil != null && model.UsuarioUnidadePerfil.Any())
                {
                    var vinculo = model.UsuarioUnidadePerfil.First();

                    model.UnidadeId = vinculo.UnidadeId;
                    model.PerfilClienteId = vinculo.PerfilId;
                }

                // Usuário não Master trabalha dentro do Cliente ativo
                if (!_usuarioLogadoService.UsuarioMaster && _usuarioLogadoService.ClienteId.HasValue)
                {
                    model.ClienteId = _usuarioLogadoService.ClienteId.Value;
                }

                await CriarViewBags(model.ClienteId);

                // Carrega Unidade e Perfil mantendo os selecionados
                if (model.ClienteId > 0)
                {
                    await CriarViewUnidadesPorCliente(model.ClienteId, model.UnidadeId);
                    await CriarViewPerfisPorCliente(model.ClienteId, model.PerfilClienteId);
                }
                else
                {
                    ViewBag.Unidades = new List<SelectListItem>();
                    ViewBag.Perfis = new List<SelectListItem>();
                }
            }

            return result;
        }

        // GET: Busca Avançada
        [HttpGet]
        public override async Task<IActionResult> BuscaAvancada(UsuarioFilterViewModel filtro)
        {
            return await base.BuscaAvancada(filtro);
        }

        // GET: Consulta
        [HttpGet]
        public override async Task<IActionResult> Consulta()
        {
            return await base.Consulta();
        }

        private async Task CriarViewBags(int? clienteSelecionado = null)
        {
            await CriarViewClientes(clienteSelecionado);
        }

        [HttpGet]
        public async Task<IActionResult> ObterUnidadesPorCliente(int clienteId)
        {
            if (clienteId <= 0)
                return Json(new List<object>());

            var unidades = await _unidadeService.ListarPorCliente(clienteId);

            var resultado = unidades.Select(x => new
            {
                id = x.Id,
                nome = x.NomeUnidade
            });

            return Json(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> ObterPerfisPorCliente(int clienteId)
        {
            if (clienteId <= 0)
                return Json(new List<object>());

            var perfis = await _perfilService.ListarPerfilPorCliente(clienteId);

            var resultado = perfis.Select(x => new
            {
                id = x.Id,
                nome = x.NomePerfil
            });

            return Json(resultado);
        }

        private async Task CriarViewPerfisPorCliente(int clienteId, int? perfilSelecionado = null)
        {
            var resultado = await _perfilService.ListarPerfilPorCliente(clienteId);

            var lista = resultado
                .Select(x => new SelectListItem
                {
                    Text = x.NomePerfil,
                    Value = x.Id.ToString(),
                    Selected = perfilSelecionado.HasValue && x.Id == perfilSelecionado.Value
                })
                .ToList();

            lista.Insert(0, new SelectListItem
            {
                Text = "- Selecione o Perfil -",
                Value = ""
            });

            ViewBag.Perfis = lista;
        }

        private async Task CriarViewUnidadesPorCliente(int clienteId, int? unidadeSelecionada = null)
        {
            var resultado = await _unidadeService.ListarPorCliente(clienteId);

            var lista = resultado
                .Select(x => new SelectListItem
                {
                    Text = x.NomeUnidade,
                    Value = x.Id.ToString(),
                    Selected = unidadeSelecionada.HasValue && x.Id == unidadeSelecionada.Value
                })
                .ToList();

            lista.Insert(0, new SelectListItem
            {
                Text = "- Selecione a Unidade -",
                Value = ""
            });

            ViewBag.Unidades = lista;
        }

        private async Task CriarViewClientes(int? clienteSelecionado = null)
        {
            var resultado = await _clienteService.ListarClientes();

            ViewBag.Clientes = resultado
                .Select(x => new SelectListItem
                {
                    Text = $"{x.NomeCliente} - {x.CNPJ}",
                    Value = x.Id.ToString(),
                    Selected = clienteSelecionado.HasValue && x.Id == clienteSelecionado.Value
                })
                .ToList();
        }
    }
}
