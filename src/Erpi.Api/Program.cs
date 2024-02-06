using System.Security.Claims;
using Erpi.Api.Extensions;
using Erpi.Api.Middlewares;
using Erpi.Trucks.Application.DependencyInjection;
using Erpi.Trucks.Application.Trucks.AddTruck;
using Erpi.Trucks.Application.Trucks.GetTruck;
using MediatR;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithAuthentication();
builder.Services.AddTrucksModule(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ApplicationAndDomainExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

// ---------------------------------------------------------------------------------------------------------------------
// Add truck
app.MapPost("truck", async ([FromBody] AddTruckCommand command, IMediator mediator, CancellationToken ct) =>
    {
        var truckResponse = await mediator.Send(command, ct);
        return Results.Ok(truckResponse);
    })
    .WithName("AddTruck")
    .WithOpenApi();

// ---------------------------------------------------------------------------------------------------------------------
// Get truck
app.MapGet("truck/{code}", async (string code, IMediator mediator, CancellationToken ct) =>
        {
            TruckResponse truckResponse = await mediator.Send(new GetTruckQuery(code), ct);
            return Results.Ok(truckResponse);
        })
    .WithName("GetResourceById")
    .WithOpenApi();

// ---------------------------------------------------------------------------------------------------------------------

app.Run();