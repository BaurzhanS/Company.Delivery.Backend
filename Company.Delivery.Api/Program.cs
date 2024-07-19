using Company.Delivery.Api.AppStart;
using Company.Delivery.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDeliveryControllers();
builder.Services.AddDeliveryApi();
builder.Services.AddDbContext<DeliveryDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: $"{Database.Name}"));

var app = builder.Build();

app.UseDeliveryApi();
app.MapControllers();

app.Run();
