using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Application.Votes.Commands.CreateVote;
using Saiketsu.Gateway.Domain.Entities;
using System.Net;
using System.Text.Json;
using System.Text;

namespace Saiketsu.Gateway.Infrastructure.Services;

public sealed class VoteService : IVoteService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public VoteService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<bool> CastVoteAsync(CreateVoteCommand command)
    {
        var httpClient = _httpClientFactory.CreateClient("VoteClient");

        var json = JsonSerializer.Serialize(command);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("/api/votes", content);

        return response.StatusCode == HttpStatusCode.OK;
    }

    public async Task<Dictionary<int, int>?> CalculateVoteAsync(int electionId)
    {
        var httpClient = _httpClientFactory.CreateClient("VoteClient");

        var response = await httpClient.GetAsync($"/api/votes/{electionId}/calculate");

        if (response.StatusCode != HttpStatusCode.OK) return null;

        var stringResponse = await response.Content.ReadAsStringAsync();
        var dictionary = JsonSerializer.Deserialize<Dictionary<int, int>>(stringResponse);
        return dictionary;
    }
}