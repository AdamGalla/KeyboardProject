using LoadBalancer;
using LoadBalancer.Strategies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var loadBalancer = new LoadBalancer.LoadBalancer() as ILoadBalancer;
loadBalancer.SetActiveStrategy(new RoundRobinStrategy());
builder.Services.AddSingleton<ILoadBalancer>(lb => loadBalancer);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
