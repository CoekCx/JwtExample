using Business;
using Infrastructure;
using JwtExamples.MinimalApi.Extensions;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddEndpoints()
    .AddBusiness()
    .AddDatabase(builder.Configuration)
    .AddAuthentication(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapEndpoints();

app.UseAuthentication();

// app.UseAuthorization();

app.Run();