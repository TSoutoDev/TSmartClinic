using AgendaApp.Api.TabelasBasicas.Extensions;
using AgendaApp.API.Extensions;
using AgendaApp.Core.Domain.Interfaces.Repositories;
using AgendaApp.Core.Domain.Middlewares;
using AgendaApp.Data.Contexts;
using AgendaApp.Data.Entities;
using AgendaApp.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
   // .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddRouting(options => options.LowercaseUrls = true);
//builder.Services.AddJwtBearer(builder.Configuration);
builder.Services.AddSwaggerDoc();
builder.Services.AddHttpContextAccessor();
//builder.Services.AddAutoMapperConfig();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSqlServerConfig(builder.Configuration);
builder.Services.AddDbContext<AgendaAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddDependencyInjection();
builder.Services.AddFluentValidationConfig();
//builder.Services.AddAuthorization();
//builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();


var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseSwaggerDoc();
//app.UseAuthentication();
//app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program { }