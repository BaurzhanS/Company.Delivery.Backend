using Company.Delivery.Api.AppStart;
using Company.Delivery.Database;
using Microsoft.EntityFrameworkCore;
using Company.Delivery.Infrastructure.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDeliveryControllers();
builder.Services.AddDeliveryApi();
builder.Services.AddDbContext<DeliveryDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: $"{Database.Name}"));

builder.Services.AddInfrastructureLayer();

var app = builder.Build();

app.UseDeliveryApi();
app.MapControllers();

app.Run();
