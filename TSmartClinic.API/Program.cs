using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TSmartClinic.Api.TabelasBasicas.Extensions;
using TSmartClinic.API.Extensions;
using TSmartClinic.Core.Domain.Middlewares;
using TSmartClinic.Data.Contexts;
using TSmartClinic.Core.Infra.CrossCutting.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddJwtBearer(builder.Configuration); // sua extensão personalizada

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddJwtBearer(builder.Configuration);
builder.Services.AddSwaggerDoc();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapperConfig();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSqlServerConfig(builder.Configuration);
builder.Services.AddDbContext<TSmartClinicContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDependencyInjection();
builder.Services.AddFluentValidationConfig();
builder.Services.AddAuthorization();
//builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

// Garante que o appsettings.json seja carregado
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);



var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseSwaggerDoc();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program { }