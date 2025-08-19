using Microsoft.AspNetCore.Authentication.Cookies;
using TSmartClinic.Core.Infra.CrossCutting.Providers;
using TSmartClinic.Presentation.Extentions;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("Chave lida: " + builder.Configuration["CryptoSettings:Chave"]);


builder.Services.AddControllersWithViews();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    //options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddAuthentication
    (CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

builder.Services.AddMvc().AddRazorRuntimeCompilation();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDependencyInjection();
builder.Services.AddUrlApi(builder.Configuration);
builder.Services.AddMapper();
builder.Services.AddFluentValidationConfig();
builder.Services.AddAutoMapper(typeof(AutoMapperExtension).Assembly);

// Registrar configurações
builder.Services.Configure<CryptoSettings>(builder.Configuration.GetSection("CryptoSettings"));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromDays(1);
    options.SlidingExpiration = true;
});

builder.Services.AddHsts(options =>
{
    options.IncludeSubDomains = true; // Inclua subdomínios no cabeçalho HSTS (opcional)
    options.MaxAge = TimeSpan.FromDays(365); // Especifique a duração do HSTS em dias
});

builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-XSRF-TOKEN"; // Nome do cabeçalho personalizado para o token anti-CSRF
    options.Cookie.Name = "XSRF-TOKEN";   // Nome do cookie para o token anti-CSRF
    options.Cookie.HttpOnly = false;       // O cookie pode ser lido por JavaScript
});

var app = builder.Build();

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
    await next.Invoke();
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHsts();
app.UseHttpsRedirection(); // Redireciona todas as solicitações HTTP para HTTPS

app.UseStaticFiles();

app.UseCookiePolicy();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
