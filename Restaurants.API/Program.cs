using Restaurants.API.Middlewares;
using Restaurants.Application.Extensions;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeders;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

// Dependency Injections
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();


// App creation
var app = builder.Build();

// Seeder
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
await seeder.Seed();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
