using System.Net;
using System.Text;
using System.Text.Json;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Application.Parties.Commands.CreateParty;
using Saiketsu.Gateway.Domain.Entities;

namespace Saiketsu.Gateway.Infrastructure.Services;

public sealed class PartyService : IPartyService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public PartyService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<PartyEntity>> GetPartiesAsync()
    {
        var httpClient = _httpClientFactory.CreateClient("PartyClient");

        var response = await httpClient.GetAsync("/api/parties");

        var stringResponse = await response.Content.ReadAsStringAsync();
        var parties = JsonSerializer.Deserialize<List<PartyEntity>>(stringResponse);

        return parties!;
    }

    public async Task<PartyEntity?> GetPartyAsync(int id)
    {
        var httpClient = _httpClientFactory.CreateClient("PartyClient");

        var response = await httpClient.GetAsync($"/api/parties/{id}");

        if (response.StatusCode != HttpStatusCode.OK) return null;

        var stringResponse = await response.Content.ReadAsStringAsync();
        var parties = JsonSerializer.Deserialize<PartyEntity>(stringResponse);

        return parties!;
    }

    public async Task<PartyEntity?> CreatePartyAsync(CreatePartyCommand command)
    {
        var httpClient = _httpClientFactory.CreateClient("PartyClient");

        var json = JsonSerializer.Serialize(command);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("/api/parties", content);

        if (response.StatusCode != HttpStatusCode.Created) return null;

        var stringResponse = await response.Content.ReadAsStringAsync();
        var party = JsonSerializer.Deserialize<PartyEntity>(stringResponse);

        return party!;
    }

    public async Task<bool> DeletePartyAsync(int id)
    {
        var httpClient = _httpClientFactory.CreateClient("PartyClient");

        var response = await httpClient.DeleteAsync($"/api/parties/{id}");

        return response.StatusCode == HttpStatusCode.OK;
    }
}