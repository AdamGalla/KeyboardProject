﻿using Microsoft.AspNetCore.Mvc;

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
    public IActionResult RegisterService([FromBody] Service service)
    {
        Console.WriteLine($"Adding Service to pool {service.Url}");

        Guid returnedId = _loadBalancer.AddService(service.Url);
        Console.WriteLine($"Registered Service Id: {returnedId}");
        return Ok(returnedId);
    }
}
