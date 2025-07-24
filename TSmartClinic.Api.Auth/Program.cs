using System.Text.Json.Serialization;
using TSmartClinic.Api.Auth.Extensions;
using TSmartClinic.API.Extensions;
using TSmartClinic.Core.Infra.CrossCutting.Criptografia;
using TSmartClinic.Core.Infra.CrossCutting.Extensions;
using TSmartClinic.Core.Infra.CrossCutting.Providers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddJwtBearer(builder.Configuration);
builder.Services.AddAutoMapperConfig();
builder.Services.AddSqlServerConfig(builder.Configuration);
builder.Services.AddSwaggerDoc();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDependencyInjection(builder.Configuration);
// Registrar configurações
builder.Services.Configure<CryptoSettings>(builder.Configuration.GetSection("CryptoSettings"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerDoc();
//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();

public partial class Program { }

