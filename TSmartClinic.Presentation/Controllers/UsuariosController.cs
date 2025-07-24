using Microsoft.AspNetCore.Mvc;
using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.Services.Interfaces;
using TSmartClinic.Presentation.ViewModels.Filters;

namespace TSmartClinic.Presentation.Controllers
{
    public class UsuariosController : BaseController<IUsuarioService, UsuarioFilterViewModel, UsuarioViewModel>
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService) : base(usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public override async  Task<IActionResult> Cadastro(UsuarioViewModel model)
        {
            //Processa a foto utilizando o serviço
            var foto = Request.Form["Foto"].ToString();
            await _usuarioService.ProcessarFotoAsync(model, foto);
            await _usuarioService.PreencherDados(model);

            return await base.Cadastro(model);
        }

        public override async Task<IActionResult> Cadastro(int? id)
        {
            await CriarViewBags();

            //preenhe a data de expiração no front end
            var result = await base.Cadastro(id) as ViewResult;
            if (result?.Model is UsuarioViewModel model)
            {
                if (!id.HasValue)
                {
                    model.DataExpiracaoSenha = DateTime.Today.AddDays(365);
                }
            }
            return await base.Cadastro(id);
        }
        private async Task CriarViewBags()
        {

            //await ConsultaViewBagCategoriaProfissional();
            //await ConsultaViewBagConselhoProfissional();
            //await ConsultaViewBagTiposLogradouro();
            //await ConsultaViewBagEstados();
        }
    }
    
}
