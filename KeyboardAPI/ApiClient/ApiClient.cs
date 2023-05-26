using KeyboardAPI.DTOs;
using Microsoft.OpenApi.Models;
using RestSharp;

namespace KeyboardAPI.ApiClient;

public class ApiClient : IApiClient
{
    public async Task<UserDTO> GetUserById(int id)
    {
        //url needs to be changed when specified in docker orchestration
        var client = new RestClient("http://localhost:9000/api");
        var request = new RestRequest("Users/{id}", Method.Get);
        request.AddParameter("id", id, ParameterType.UrlSegment);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Accept", "application/json");
        var response = await client.ExecuteGetAsync<UserDTO>(request);

        return response.Data;
    }
}