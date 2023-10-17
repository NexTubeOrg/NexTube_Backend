using NexTube.Application.Common.Mappings;
using NexTube.Persistence.Common.Extensions;
using NexTube.Persistence.Data.Contexts;
using NexTube.Persistence.Data.Seeders;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// get configuration
var configuration = builder.Configuration;

// ensure all required settings exist
configuration.EnsureExistence("appsettings.Development.json");

// Add Clean-Architecture layers
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(configuration);

builder.Services.AddAutoMapper(config => {
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(UserDbContext).Assembly));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.SeedData();

app.Run();
