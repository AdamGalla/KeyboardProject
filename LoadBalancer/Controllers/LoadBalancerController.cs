using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LoadBalancer.Controllers;
[Route("api/[controller]")]
[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class LoadBalancerController : ControllerBase
{
    private readonly ILoadBalancer _loadBalancer;

    public LoadBalancerController(ILoadBalancer loadBalancer)
    {
        _loadBalancer = loadBalancer;
    }

    [Route("{**path}")]
    public async Task<IActionResult> RouteRequest()
    {
        do
        {
            var service = _loadBalancer.GetNextService();
            if (service is null)
            {
                return NotFound();
            }

            var response = await RerouteMessageRequest(Request, service);
            if (response is null)
            {
                _ = _loadBalancer.RemoveService(service.Id);
            }
            else
            {
                return response;
            }
        } while (_loadBalancer.GetAllServices().Any());

        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    private async Task<IActionResult?> RerouteMessageRequest(HttpRequest request, Service service)
    {
        var requestMessage = BuildRequestMessage(request, service.Url);

        using var httpClient = new HttpClient();

        var timer = Stopwatch.StartNew();
        var response = await httpClient.SendAsync(requestMessage);
        timer.Stop();
        service.AddLatestResponseTime(timer.ElapsedMilliseconds);

        return await GetActionResult(response);
    }

    private async Task<IActionResult?> GetActionResult(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, response.Content.Headers.ContentType!.MediaType!);
        }
        else if((int)response.StatusCode >= 500)
        {
            return null;
        }
        return StatusCode((int)response.StatusCode);
    }

    private static HttpRequestMessage BuildRequestMessage(HttpRequest request, string serviceUrl)
    {
        string path = request.Path.Value!.Replace("LoadBalancer/", string.Empty, StringComparison.OrdinalIgnoreCase);
        var targetUrl = $"http://{serviceUrl}{path}";
        Console.WriteLine($"Built new target url: {targetUrl}");
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
