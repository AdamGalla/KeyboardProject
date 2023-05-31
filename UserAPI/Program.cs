using Microsoft.EntityFrameworkCore;
using RestSharp;
using UserAPI.Data;
using UserAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Register the service to the loadbalancer
Console.WriteLine("Hostname: " + Environment.MachineName);
var client = new RestClient("http://loadbalancer:9080");
var request = new RestRequest("LoadBalancerServices/RegisterService", Method.Post).AddJsonBody(new { Url = Environment.MachineName });
_ = await client.ExecuteAsync(request);

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
