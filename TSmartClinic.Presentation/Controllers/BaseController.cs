using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.Services.Interfaces;
using TSmartClinic.Presentation.ViewModels.Filters;

namespace TSmartClinic.Presentation.Controllers
{
    public abstract class BaseController<TService, TFilterViewModel, TViewModel> : Controller
        where TService : IBaseService<TFilterViewModel,TViewModel>
        where TFilterViewModel : BaseFilterViewModel
        where TViewModel : BaseViewModel
    {
        private TService _service;
        protected TService Servico => _service;
        protected TViewModel Model { get; set; }

        protected BaseController(TService service)
        {
            _service = service;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Consulta()
        {
            ViewBag.BuscaAvancada = false;
            return View();
        }

        [HttpPost]
        public virtual async Task<IActionResult> BuscaAvancada(TFilterViewModel filtro)
        {
            return await Busca(filtro, true);
        }

        private async Task<IActionResult> Busca(TFilterViewModel filtro, bool buscaAvancada)
        {
            RedefinirViewBags(filtro, buscaAvancada);

            //Grava o filtro na sessão
            HttpContext.Session.SetString("Filtro", JsonConvert.SerializeObject(filtro));

            //Chama o gateway, que chamará a API para filtrar os registros
            var retorno = await _service.Listar(filtro);

            if (retorno.Sucesso)
            {
                //Grava as informações de paginação no objeto response
                retorno.QuantidadeRegistros = retorno.Itens.Count;

                if (retorno.QuantidadeRegistros == 0)
                {
                    TempData["MensagemAlerta"] = "Não foram encontrados registros para os critérios informados.";
                    return View("Consulta", null);
                }
                else if (retorno.QuantidadeRegistros > 1000)
                {
                    TempData["MensagemAlerta"] = "Exibindo os 1000 primeiros registros retornados pela pesquisa. Caso necessário, refine os filtros utilizados.";
                    retorno.Itens = retorno.Itens?.Take(1000).ToList();
                }

                return View("Consulta", retorno);
            }
            else
            {
                TempData["MensagemErro"] = retorno.Mensagem;
                return View("Consulta", null);
            }
        }


        public virtual async Task<IActionResult> Cadastro(int? id)
        {
            if (id.HasValue)
            {
                var retorno = await _service.ObterPorId(id.Value);

                if (retorno.Sucesso)
                    Model = retorno.Itens.FirstOrDefault();
                else
                {
                    TempData["MensagemErro"] = retorno.Mensagem;
                    Model = (TViewModel)Activator.CreateInstance(typeof(TViewModel));
                }
            }
            else
                if (Model == null) Model = (TViewModel)Activator.CreateInstance(typeof(TViewModel));

            return View(Model);
        }


        [HttpPost]
        public virtual async Task<IActionResult> Cadastro(TViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ResponseViewModel<TViewModel> retorno = null;

                    if (!model.Id.HasValue) //Inclusão
                    {
                        retorno = await _service.Inserir(model);

                        if (retorno.Sucesso) model = (TViewModel)Activator.CreateInstance(typeof(TViewModel));
                    }
                    else //Alteração
                        retorno = await _service.Atualizar(model.Id.Value, model);

                    if (retorno.Sucesso)
                        TempData["MensagemSucesso"] = "Gravação realizada com sucesso.";
                    else
                        TempData["MensagemErro"] = retorno.Mensagem;

                    ModelState.Clear();
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }
            else
            {
                TempData["MensagemAlerta"] = "Verifique o preenchimento dos dados no formulário.";
            }

            return View(model);
        }


        [HttpPost]
        public virtual async Task<IActionResult> Excluir(TViewModel model)
        {
            var retorno = await _service.Excluir(model.Id.Value);

            if (retorno.Sucesso)
            {
                TempData["MensagemSucesso"] = "Exclusão realizada com sucesso.";
                return RedirectToAction("Consulta");
            }
            else
            {
                TempData["MensagemErro"] = retorno.Mensagem;
                return View("Cadastro", model);
            }
        }


        protected void RedefinirViewBags(TFilterViewModel filtro, bool buscaAvancada)
        {
            ViewBag.BuscaAvancada = buscaAvancada;
            ViewBag.Filtro = filtro;
        }
    }
}
