using Microsoft.Extensions.Options;
using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.Services.Interfaces;
using TSmartClinic.Presentation.Settings;
using TSmartClinic.Presentation.ViewModels.Filters;

namespace TSmartClinic.Presentation.Services
{
    public class ConvenioService : BaseService<BaseFilterViewModel, ConvenioViewModel>, IConvenioService
    {
        public ConvenioService(IAccessTokenService accessTokenService, IOptions<UrlApiSettings>? urlApiSettings) : base(accessTokenService, urlApiSettings, "convenios")
        {
        }
        public async Task<List<ConvenioViewModel>> ListarConvenios()
        {
            var filtro = new BaseFilterViewModel
            {
                Ativo = true,
                OperadorLogico = "AND",
                PaginaAtual = 0,
                ItensPorPagina = 0
            };

            var resultado = await Listar(filtro);

            return resultado?.Itens ?? new List<ConvenioViewModel>();
        }
    }
}