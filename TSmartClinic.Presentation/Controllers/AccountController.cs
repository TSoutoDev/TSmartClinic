using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using TSmartClinic.Core.Domain.Interfaces.Providers;
using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace TSmartClinic.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccessTokenService _accessTokenService;
        private readonly IAutenticacaoService _autenticacaoService;
        private readonly ICriptografiaProvider _criptografiaProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(
            IAccessTokenService accessTokenService,
            IAutenticacaoService autenticacaoService,
            ICriptografiaProvider criptografiaProvider,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _accessTokenService = accessTokenService;
            _autenticacaoService = autenticacaoService;
            _criptografiaProvider = criptografiaProvider;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Login()
        {
            if (Request.Cookies["TSmartClinic-autx"] == null)
                GravarCookieTentativa(1);

            return View(new AccountViewModel());
        }




        [HttpPost]
        public IActionResult Login(AccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = _autenticacaoService.Logar(model).Result; // da erro

                if (!response.Sucesso)
                {
                    switch (response.StatusCode)
                    {
                        case 404:
                            IncrementaTentativa();
                            break;

                        default:
                            TempData["MensagemErro"] = response.Mensagem;
                            break;
                    }
                }
                else
                {
                    try
                    {
                        var autenticacao = response.Itens.FirstOrDefault();

                        _accessTokenService.Salvar(autenticacao.AccessToken);

                        var handler = new JwtSecurityTokenHandler();
                        var token = handler.ReadJwtToken(autenticacao.AccessToken);

                        var permissoes = token.Claims
                            .Where(x => x.Type == "permissao")
                            .SelectMany(x => x.Value.Split(',', StringSplitOptions.RemoveEmptyEntries))
                            .Distinct()
                            .ToList();

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, autenticacao.Nome),
                            new Claim(ClaimTypes.Email, autenticacao.Email)
                        };

                        claims.Add(new Claim("permissao", string.Join(',', permissoes)));

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);

                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        //Excluir o cookie de tentativas
                        if (Request.Cookies["TSmartClinic-autx"] != null) Response.Cookies.Delete("TSmartClinic-autx", OpcoesCookies());

                        return RedirectToAction("Index", "Home");
                       // return RedirectToAction("Login", "Account");
                    }
                    catch (Exception e)
                    {
                        TempData["MensagemErro"] = e.Message;
                    }
                }
            }
            else
            {
                TempData["MensagemAlerta"] = "Verifique o preenchimento dos dados no formulário.";
            }

            return View(model);

        }

        public IActionResult Logout()
        {
            //HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //HttpContext.Response.Cookies.Delete("peoplenetsst-autx", OpcoesCookies());

            //HttpContext.Session.Clear();

            _autenticacaoService.Logout();

            return RedirectToAction("Login", "Account");
        }


        private void GravarCookieTentativa(int tentativa)
        {
            Response.Cookies.Append(
                "TSmartClinic-autx",
                _criptografiaProvider.Criptografar($"{DateTime.Now.ToString("yyyyMMddHHmmss")}-{tentativa.ToString()}-3"),
                OpcoesCookies()
            );
        }




        private CookieOptions OpcoesCookies()
        {
            return new CookieOptions
            {
                HttpOnly = false,
                Expires = DateTime.UtcNow.AddHours(12),
                IsEssential = true,
                Secure = true
            };
        }

        private void IncrementaTentativa()
        {
            if (Request.Cookies["TSmartClinic-autx"] != null)
            {
                try
                {
                    var cookie = _criptografiaProvider.Decriptografar(Request.Cookies["TSmartClinic-autx"].ToString());
                    var tentativa = Int32.Parse(cookie.Split("-")[1]);
                    var totalTentativas = Int32.Parse(cookie.Split("-")[2]);

                    if (tentativa < totalTentativas)
                        TempData["MensagemErro"] = $"Senha de acesso inválida. Você tem mais {totalTentativas - tentativa} tentativa(s) até o seu usuário ser bloqueado.";
                    else
                        TempData["MensagemErro"] = $"Senha de acesso inválida. Seu usuário foi bloqueado.";

                    GravarCookieTentativa(++tentativa);
                }
                catch (FormatException e)
                {
                    TempData["MensagemErro"] = "Erro de acesso aos cookies.";
                }
            }
        }
    }
}