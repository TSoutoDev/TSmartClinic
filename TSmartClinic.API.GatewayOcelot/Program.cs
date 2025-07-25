using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using TSmartClinic.Core.Infra.CrossCutting.Extensions;



var builder = WebApplication.CreateBuilder(args);


builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddOcelot($"{builder.Environment.ContentRootPath}/config/{builder.Environment.EnvironmentName}", builder.Environment)
    .AddEnvironmentVariables();


// Agora sim pode usar a config carregada corretamente
builder.Services.AddJwtBearer(builder.Configuration);

builder.Services.AddOcelot(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});


//builder.Services
//    .AddAuthentication()
//    .AddJwtBearer("Bearer", options => { /* configuração do JWT */ });
//builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();


app.UseCors("AllowSpecificOrigin");

app.MapGet("/", () => "Hello World!");
app.MapControllers();

await app.UseOcelot();

app.Run();
