using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using TSmartClinic.Core.Infra.CrossCutting.Extensions;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwtBearer(builder.Configuration);

//builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddOcelot($"{builder.Environment.ContentRootPath}/config/{builder.Environment.EnvironmentName}", builder.Environment)
    .AddEnvironmentVariables();

builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowSpecificOrigin");

app.MapGet("/", () => "Hello World!");
app.MapControllers();
await app.UseOcelot();

app.Run();
