using Microsoft.EntityFrameworkCore;
using RestSharp;
using UserAPI.Data;
using UserAPI.Models;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("Hostname: " + Environment.MachineName);
var client = new RestClient("http://users-loadbalancer");
var request = new RestRequest("api/LoadBalancerServices/RegisterService", Method.Post).AddJsonBody(new { Url = Environment.MachineName });
var result = await client.ExecuteAsync(request);

string error = result.ErrorMessage ?? "None";
Console.WriteLine($"Posted registration: {result.IsSuccessful}; Errors: {error}");

builder.Services.AddDbContext<UserApiContext>(opt => opt.UseInMemoryDatabase("UsersDb"));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddTransient<IDbInitializer, DbInitializer>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetService<UserApiContext>();
    var dbInitializer = services.GetService<IDbInitializer>();
    dbInitializer.Initialize(dbContext);
}

app.UseAuthorization();

app.MapControllers();

app.Run();
