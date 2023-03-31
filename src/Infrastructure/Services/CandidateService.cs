using System.Net;
using System.Text;
using System.Text.Json;
using Saiketsu.Gateway.Application.Candidates.Commands.CreateCandidate;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Domain.Entities;

namespace Saiketsu.Gateway.Infrastructure.Services;

public sealed class CandidateService : ICandidateService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CandidateService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<CandidateEntity?> GetCandidateAsync(int id)
    {
        var httpClient = _httpClientFactory.CreateClient("CandidateClient");

        var response = await httpClient.GetAsync($"/api/candidates/{id}");

        if (response.StatusCode != HttpStatusCode.OK) return null;

        var stringResponse = await response.Content.ReadAsStringAsync();
        var candidate = JsonSerializer.Deserialize<CandidateEntity>(stringResponse);

        return candidate!;
    }

    public async Task<List<CandidateEntity>> GetCandidatesAsync()
    {
        var httpClient = _httpClientFactory.CreateClient("CandidateClient");

        var response = await httpClient.GetAsync("/api/candidates");

        if (response.StatusCode != HttpStatusCode.OK) return new List<CandidateEntity>();

        var stringResponse = await response.Content.ReadAsStringAsync();
        var candidates = JsonSerializer.Deserialize<List<CandidateEntity>>(stringResponse);

        return candidates!;
    }

    public async Task<CandidateEntity?> CreateCandidateAsync(CreateCandidateCommand command)
    {
        var httpClient = _httpClientFactory.CreateClient("CandidateClient");

        var json = JsonSerializer.Serialize(command);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("/api/candidates", content);

        if (response.StatusCode != HttpStatusCode.Created) return null;

        var stringResponse = await response.Content.ReadAsStringAsync();
        var candidate = JsonSerializer.Deserialize<CandidateEntity>(stringResponse);

        return candidate!;
    }

    public async Task<bool> DeleteCandidateAsync(int id)
    {
        var httpClient = _httpClientFactory.CreateClient("CandidateClient");

        var response = await httpClient.DeleteAsync($"/api/candidates/{id}");

        return response.StatusCode == HttpStatusCode.OK;
    }
}