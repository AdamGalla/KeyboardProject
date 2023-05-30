using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoadBalancer.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LoadBalancerServicesController : ControllerBase
{
    private readonly ILoadBalancer _loadBalancer;

    public LoadBalancerServicesController(ILoadBalancer loadBalancer)
    {
        _loadBalancer = loadBalancer;
    }

    [HttpPost("RegisterService")]
    public IActionResult RegisterService([FromBody] string url)
    {
        Console.WriteLine($"Adding Service to pool {url}");

        Guid returnedId = _loadBalancer.AddService(url);
        Console.WriteLine($"Registered Service Id: {returnedId}");
        return Ok(returnedId);
    }
}
