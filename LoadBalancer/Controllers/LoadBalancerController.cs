using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LoadBalancer.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LoadBalancerController : ControllerBase
{
    private readonly ILoadBalancer _loadBalancer;

    public LoadBalancerController(ILoadBalancer loadBalancer)
    {
        _loadBalancer = loadBalancer;
    }

    [Route("{serviceName}/{**path}")]
    public async Task<IActionResult> RouteRequest(string serviceName)
    {
        var service = _loadBalancer.GetNextService();
        if (service is null)
        {
            return NotFound();
        }

        var requestMessage = BuildRequestMessage(Request, service);

        using var httpClient = new HttpClient();

        var timer = Stopwatch.StartNew();
        var response = await httpClient.SendAsync(requestMessage);
        timer.Stop();
        service.AddLatestResponseTime(timer.ElapsedMilliseconds);

        return await GetStatusCode(response);
    }

    private async Task<IActionResult> GetStatusCode(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, response.Content.Headers.ContentType!.MediaType!);
        }

        return StatusCode((int)response.StatusCode);
    }

    private static HttpRequestMessage BuildRequestMessage(HttpRequest request, Service service)
    {
        var targetUrl = $"{service.Url}/{request.Path.Value}";
        var requestMessage = new HttpRequestMessage(new HttpMethod(request.Method), targetUrl)
        {
            Content = new StreamContent(request.Body)
        };

        foreach (var header in request.Headers)
        {
            requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
        }
        return requestMessage;
    }
}
