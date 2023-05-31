using KeyboardAPI.ApiClient;
using KeyboardAPI.Data;
using KeyboardAPI.Models;
using Microsoft.EntityFrameworkCore;
using RestSharp;

var builder = WebApplication.CreateBuilder(args);

// Register the service to the loadbalancer
Console.WriteLine("Hostname: " + Environment.MachineName);
var client = new RestClient("http://keyboards-loadbalancer");
var request = new RestRequest("api/LoadBalancerServices/RegisterService", Method.Post).AddJsonBody(new { Url = Environment.MachineName });
var result = await client.ExecuteAsync(request);

string error = result.ErrorMessage ?? "None";
Console.WriteLine($"Posted registration: {result.IsSuccessful}; Errors: {error}");

// Add services to the container.
//Register ApiClient for dependency injection
builder.Services.AddScoped<IApiClient>(apiClient => new ApiClient());

builder.Services.AddDbContext<KeyboardApiContext>(opt => opt.UseInMemoryDatabase("KeyboardsDd"));
// Register repositories for dependency injection
builder.Services.AddScoped<IRepository<Keyboard>, KeyboardRepository>();
// Register database initializer for dependency injection
builder.Services.AddTransient<IDbInitializer, DbInitializer>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Configure cors
app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Initialize the database.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetService<KeyboardApiContext>();
    var dbInitializer = services.GetService<IDbInitializer>();
    dbInitializer.Initialize(dbContext);
}

app.UseAuthorization();

app.MapControllers();

app.Run();
