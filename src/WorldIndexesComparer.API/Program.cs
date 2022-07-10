using Microsoft.AspNetCore.Mvc;
using WorldIndexesComparer.Application.Extensions;
using WorldIndexesComparer.Infrastructure.Data.Modules;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApiVersioning(cfg =>
{
    cfg.DefaultApiVersion = new ApiVersion(1, 0);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddAppServices()
    .AddClientsConfiguration(builder.Configuration)
    .AddDataInfrastructure(builder.Configuration)
    .AddCQRSConfiguration();

var app = builder.Build();

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
