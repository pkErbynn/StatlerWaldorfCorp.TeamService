using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StatlerWaldorfCorp.TeamService.LocationClient;
using StatlerWaldorfCorp.TeamService.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Configure logging
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITeamRepository, InMemoryTeamRepository>();

var locationUrl = builder.Configuration.GetSection("location:url").Value;
//var locationUrl = builder.Configuration["location:url"];

builder.Services.AddSingleton<ILocationClient>(new HttpLocationClient(locationUrl));

var app = builder.Build();

// Log locationUrl during startup
var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Location URL: {0}", locationUrl);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

