using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using TSmartClinic.Core.Domain.Interfaces.Providers;
using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.Services.Interfaces;


namespace TSmartClinic.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccessTokenService _accessTokenService;
        private readonly IAutenticacaoService _autenticacaoService;
        private readonly ICriptografiaProvider _criptografiaProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUsuarioService _usuarioService;

        public AccountController(
            IAccessTokenService accessTokenService,
            IAutenticacaoService autenticacaoService,
            ICriptografiaProvider criptografiaProvider,
            IHttpContextAccessor httpContextAccessor,
            IUsuarioService usuarioService



        )
        {
            _accessTokenService = accessTokenService;
            _autenticacaoService = autenticacaoService;
            _criptografiaProvider = criptografiaProvider;
            _httpContextAccessor = httpContextAccessor;
            _usuarioService = usuarioService;
        }

        public IActionResult Login()
        {
            if (Request.Cookies["TSmartClinic-autx"] == null)
                GravarCookieTentativa(1);

            return View(new AccountViewModel());
        }




        [HttpPost]
        public async Task<IActionResult> Login(AccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["MensagemAlerta"] = "Verifique o preenchimento dos dados no formulário.";
                return View(model);
            }

            if (ModelState.IsValid)
            {
                // Use await em vez de .Result
                var response = await _autenticacaoService.Logar(model);

                if (!response.Sucesso)
                {
                    switch (response.StatusCode)
                    {
                        case 404:
                            IncrementaTentativa();
                            TempData["MensagemErro"] = "Usuário não encontrado.";
                            break;

                        case 401:
                            TempData["MensagemErro"] = "E-mail ou senha inválidos.";
                            break;

                        default:
                            TempData["MensagemErro"] = response.Mensagem ?? "Erro ao autenticar. Tente novamente.";
                            break;
                    }
                    return View(model); // retorna para a tela com mensagem
                }

                else
                {
                    try
                    {

                        // Aqui você busca o usuário no banco (simulado abaixo)
                        // var usuario = _usuarioService.ObterPorEmail(model.Email);

                        var autenticacao = response.Itens.FirstOrDefault();

                        if (autenticacao.NecessitaSelecionarUnidade)
                        {
                            var loginPendenteJson = JsonSerializer.Serialize(autenticacao);
                            HttpContext.Session.SetString("LoginPendente", loginPendenteJson);

                            var viewModel = new SelecionarUnidadeViewModel
                            {
                                Unidades = autenticacao.Unidades
                            };

                            return View("SelecionarUnidade", viewModel);
                        }

                        // Converte seguro para int
                        if (!int.TryParse(autenticacao.IdUsuario?.ToString(), out int usuarioId))
                        {
                            TempData["MensagemErro"] = "Id do usuário inválido.";
                            return RedirectToAction("Login");
                        }

                        // Se primeiro acesso, redireciona para redefinir senha
                        if (autenticacao.PrimeiroAcesso)
                        {
                            var primeiroAcesso = new ResetSenhaEPrimeiroAcessoViewModel
                            {
                                IdUsuario = usuarioId, // Converte string para int
                                Email = autenticacao.Email
                            };
                            return View("PrimeiroAcesso", primeiroAcesso);
                        }

                        if (!autenticacao.UnidadeId.HasValue)
                        {
                            TempData["MensagemErro"] = "Unidade não identificada.";
                            return RedirectToAction("Login");
                        }

                        var unidade = autenticacao.Unidades.FirstOrDefault(x => x.Id == autenticacao.UnidadeId.Value);

                        if (unidade == null)
                        {
                            TempData["MensagemErro"] = "Unidade vinculada ao usuário não encontrada.";
                            return RedirectToAction("Login");
                        }

                        var cliente = autenticacao.ListClientes.FirstOrDefault(x => x.Id == unidade.ClienteId);

                        if (cliente == null)
                        {
                            TempData["MensagemErro"] = "Cliente da unidade não encontrado.";
                            return RedirectToAction("Login");
                        }

                        _accessTokenService.Salvar(autenticacao.AccessToken);
                        var permissoes = autenticacao.Permissoes ?? new List<string>();

                        var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, autenticacao.Nome),
                                new Claim(ClaimTypes.Email, autenticacao.Email),
                                new Claim("Usuario_Id", autenticacao.IdUsuario.ToString()),
                                new Claim("Cliente_NichoId", cliente.NichoId.ToString() ?? ""),
                                new Claim("Usuario_Tipo", autenticacao.TipoUsuario),
                                new Claim("Cliente_Nome", cliente.NomeCliente ?? ""),
                                new Claim("Cliente_Cnpj", cliente.CNPJ ?? ""),
                                new Claim("Cliente_Id", cliente.Id.ToString() ?? ""),
                                new Claim("Unidade_Id", unidade.Id.ToString()),
                                new Claim("Unidade_Nome", unidade.NomeUnidade ?? ""),
                                new Claim("Usuario_Email", autenticacao.Email ?? "")
                            };

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);

                       await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


                        //Excluir o cookie de tentativas
                        if (Request.Cookies["TSmartClinic-autx"] != null) Response.Cookies.Delete("TSmartClinic-autx", OpcoesCookies());

                        // Dados da sessão
                        HttpContext.Session.SetString("Usuario_Nome", autenticacao.Nome);
                        HttpContext.Session.SetString("Usuario_Email", autenticacao.Email);
                        HttpContext.Session.SetString("Usuario_Id", autenticacao.IdUsuario.ToString());
                        HttpContext.Session.SetString("Usuario_Tipo", autenticacao.TipoUsuario);
                        HttpContext.Session.SetString("Cliente_Nome", cliente.NomeCliente);
                        HttpContext.Session.SetString("Cliente_Id", cliente.Id.ToString());
                        HttpContext.Session.SetString("Cliente_NichoId", cliente.NichoId.ToString());
                        // Permissões da Presentation
                        HttpContext.Session.SetString("Permissoes", string.Join(",", permissoes));
                        HttpContext.Session.SetString("Unidades", JsonSerializer.Serialize(autenticacao.Unidades));

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


        public async Task<IActionResult> Logout()
        {
            // Primeiro chama o logout remoto enquanto o token ainda existe
            await _autenticacaoService.Logout();

            // Depois encerra a autenticação local
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.Remove("LoginPendente");

            // Limpa os dados da sessão da Presentation
            HttpContext.Session.Clear();

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

        [HttpGet("account/primeiro-acesso")]
        public IActionResult PrimeiroAcesso([FromQuery] string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                TempData["MensagemErro"] = "Link inválido ou expirado. Solicite um novo e-mail de primeiro acesso.";
                return RedirectToAction("Login");
            }

            var vm = new ResetSenhaEPrimeiroAcessoViewModel { Token = token };
            return View("PrimeiroAcesso", vm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost("account/primeiro-acesso")]
        public async Task<IActionResult> PrimeiroAcesso(ResetSenhaEPrimeiroAcessoViewModel model,
        [FromServices] TSmartClinic.Presentation.Services.Interfaces.IUsuarioService usuarioService)
        {
            // campos não usados neste fluxo
            ModelState.Remove(nameof(ResetSenhaEPrimeiroAcessoViewModel.Email));
            ModelState.Remove(nameof(ResetSenhaEPrimeiroAcessoViewModel.IdUsuario));

            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var resp = await usuarioService.DefinirSenhaTokenAsync(model.Token, model.NovaSenha);
                if (!resp.Sucesso)
                {
                    TempData["MensagemErro"] = resp.Mensagem ?? "Não foi possível definir a senha.";
                    return View("PrimeiroAcesso", model);
                }

                TempData["MensagemSucesso"] = "Senha definida com sucesso! Faça login novamente.";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return View(model);
            }
        }

        // GET - Tela "Esqueci minha senha"
        [HttpGet("account/esqueci-senha")]
        public IActionResult EsqueciSenha()
        {
            return View(new EsqueciSenhaViewModel());
        }

        // POST - Recebe e-mail e envia o link com token
        [HttpPost("account/esqueci-senha")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EsqueciSenha(EsqueciSenhaViewModel model, [FromServices] IUsuarioService usuarioService)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var resp = await usuarioService.GerarTokenResetSenhaAsync(model.Email);

                if (!resp.Sucesso)
                {
                    TempData["MensagemErro"] = "Não foi possível enviar o e-mail de redefinição.";
                    return View(model);
                }

                TempData["MensagemSucesso"] = "Um link de redefinição foi enviado para seu e-mail.";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SelecionarUnidade(SelecionarUnidadeViewModel model)
        {
            if (!model.UnidadeId.HasValue)
            {
                ModelState.AddModelError("", "Selecione uma unidade.");
                return View(model);
            }

            var loginPendenteJson = HttpContext.Session.GetString("LoginPendente");

            if (string.IsNullOrWhiteSpace(loginPendenteJson))
                return RedirectToAction("Login");

            var loginPendente = JsonSerializer.Deserialize<AccountViewModel>(loginPendenteJson);

            if (loginPendente == null || string.IsNullOrWhiteSpace(loginPendente.TokenSelecaoUnidade))
                return RedirectToAction("Login");

            var response = await _autenticacaoService.SelecionarUnidade(loginPendente.TokenSelecaoUnidade, model.UnidadeId.Value, model.DefinirComoPadrao);

            if (!response.Sucesso || response.Itens == null || !response.Itens.Any())
            {
                model.Unidades = loginPendente.Unidades;
                ModelState.AddModelError("", response.Mensagem ?? "Não foi possível selecionar a unidade.");
                return View(model);
            }

            var autenticacao = response.Itens.First();

            if (!autenticacao.UnidadeId.HasValue)
            {
                TempData["MensagemErro"] = "Unidade não identificada.";
                return RedirectToAction("Login");
            }

            var unidade = autenticacao.Unidades.FirstOrDefault(x => x.Id == autenticacao.UnidadeId.Value);

            if (unidade == null)
            {
                TempData["MensagemErro"] = "Unidade selecionada não encontrada.";
                return RedirectToAction("Login");
            }

            var cliente = autenticacao.ListClientes.FirstOrDefault(x => x.Id == unidade.ClienteId);

            if (cliente == null)
            {
                TempData["MensagemErro"] = "Cliente da unidade selecionada não encontrado.";
                return RedirectToAction("Login");
            }

            _accessTokenService.Salvar(autenticacao.AccessToken);

            var permissoes = autenticacao.Permissoes ?? new List<string>();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, autenticacao.Nome ?? ""),
                new Claim(ClaimTypes.Email, autenticacao.Email ?? ""),
                new Claim("Usuario_Id", autenticacao.IdUsuario.ToString() ?? ""),
                new Claim("Usuario_Tipo", autenticacao.TipoUsuario ?? ""),
                new Claim("Cliente_Nome", cliente.NomeCliente ?? ""),
                new Claim("Cliente_Cnpj", cliente.CNPJ ?? ""),
                new Claim("Cliente_Id", cliente.Id.ToString()),
                new Claim("Cliente_NichoId", cliente.NichoId.ToString()),
                new Claim("Unidade_Id", unidade.Id.ToString()),
                new Claim("Unidade_Nome", unidade.NomeUnidade ?? ""),
                new Claim("Usuario_Email", autenticacao.Email ?? "")
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            if (Request.Cookies["TSmartClinic-autx"] != null)
                Response.Cookies.Delete("TSmartClinic-autx", OpcoesCookies());

            HttpContext.Session.SetString("Usuario_Nome", autenticacao.Nome ?? "");
            HttpContext.Session.SetString("Usuario_Email", autenticacao.Email ?? "");
            HttpContext.Session.SetString("Usuario_Id", autenticacao.IdUsuario.ToString() ?? "");
            HttpContext.Session.SetString("Usuario_Tipo", autenticacao.TipoUsuario ?? "");

            HttpContext.Session.SetString("Cliente_Nome", cliente.NomeCliente ?? "");
            HttpContext.Session.SetString("Cliente_Id", cliente.Id.ToString());
            HttpContext.Session.SetString("Cliente_NichoId", cliente.NichoId.ToString());

            HttpContext.Session.SetString("Unidade_Id", unidade.Id.ToString());
            HttpContext.Session.SetString("Unidade_Nome", unidade.NomeUnidade ?? "");

            HttpContext.Session.SetString("Permissoes", string.Join(",", permissoes));

            HttpContext.Session.Remove("LoginPendente");

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TrocarUnidade(int unidadeId, string? returnUrl = null)
        {
            var response = await _autenticacaoService.TrocarUnidade(unidadeId);

            if (!response.Sucesso || response.Itens == null || !response.Itens.Any())
            {
                TempData["MensagemErro"] = response.Mensagem ?? "Não foi possível trocar a unidade.";
                return RedirectToAction("Index", "Home");
            }

            var autenticacao = response.Itens.First();

            if (!autenticacao.UnidadeId.HasValue)
            {
                TempData["MensagemErro"] = "Unidade não identificada.";
                return RedirectToAction("Index", "Home");
            }

            var unidade = autenticacao.Unidades.FirstOrDefault(x => x.Id == autenticacao.UnidadeId.Value);

            if (unidade == null)
            {
                TempData["MensagemErro"] = "Unidade selecionada não encontrada.";
                return RedirectToAction("Index", "Home");
            }

            var cliente = autenticacao.ListClientes.FirstOrDefault(x => x.Id == unidade.ClienteId);

            if (cliente == null)
            {
                TempData["MensagemErro"] = "Cliente da unidade selecionada não encontrado.";
                return RedirectToAction("Index", "Home");
            }

            _accessTokenService.Salvar(autenticacao.AccessToken);

            var permissoes = autenticacao.Permissoes ?? new List<string>();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, autenticacao.Nome ?? ""),
                new Claim(ClaimTypes.Email, autenticacao.Email ?? ""),
                new Claim("Usuario_Id", autenticacao.IdUsuario.ToString() ?? ""),
                new Claim("Usuario_Tipo", autenticacao.TipoUsuario ?? ""),
                new Claim("Cliente_Nome", cliente.NomeCliente ?? ""),
                new Claim("Cliente_Cnpj", cliente.CNPJ ?? ""),
                new Claim("Cliente_Id", cliente.Id.ToString()),
                new Claim("Cliente_NichoId", cliente.NichoId.ToString()),
                new Claim("Unidade_Id", unidade.Id.ToString()),
                new Claim("Unidade_Nome", unidade.NomeUnidade ?? ""),
                new Claim("Usuario_Email", autenticacao.Email ?? "")
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            HttpContext.Session.SetString("Usuario_Nome", autenticacao.Nome ?? "");
            HttpContext.Session.SetString("Usuario_Email", autenticacao.Email ?? "");
            HttpContext.Session.SetString("Usuario_Id", autenticacao.IdUsuario.ToString() ?? "");
            HttpContext.Session.SetString("Usuario_Tipo", autenticacao.TipoUsuario ?? "");

            HttpContext.Session.SetString("Cliente_Nome", cliente.NomeCliente ?? "");
            HttpContext.Session.SetString("Cliente_Id", cliente.Id.ToString());
            HttpContext.Session.SetString("Cliente_NichoId", cliente.NichoId.ToString());

            HttpContext.Session.SetString("Unidade_Id", unidade.Id.ToString());
            HttpContext.Session.SetString("Unidade_Nome", unidade.NomeUnidade ?? "");

            HttpContext.Session.SetString("Permissoes", string.Join(",", permissoes));
            HttpContext.Session.SetString("Unidades", JsonSerializer.Serialize(autenticacao.Unidades));

            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                return LocalRedirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }
    }
}