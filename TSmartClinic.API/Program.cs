using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Serialization;
using TSmartClinic.Api.TSmartClinic.Extensions;
using TSmartClinic.API.Extensions;
using TSmartClinic.API.Handles;
using TSmartClinic.Core.Domain.Middlewares;
using TSmartClinic.Core.Infra.CrossCutting.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddJwtBearer(builder.Configuration);
builder.Services.AddSwaggerDoc();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapperConfig();
builder.Services.AddSqlServerConfig(builder.Configuration);
builder.Services.AddDependencyInjection();
builder.Services.AddFluentValidationConfig();
builder.Services.AddAuthorization();
builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseSwaggerDoc();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program { }