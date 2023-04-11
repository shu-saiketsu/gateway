using System.Net;
using System.Text;
using System.Text.Json;
using System.Web;
using Saiketsu.Gateway.Application.Elections.Commands.AddCandidateToElection;
using Saiketsu.Gateway.Application.Elections.Commands.AddUserToElection;
using Saiketsu.Gateway.Application.Elections.Commands.CreateElection;
using Saiketsu.Gateway.Application.Elections.Commands.RemoveCandidateFromElection;
using Saiketsu.Gateway.Application.Elections.Commands.RemoveUserFromElection;
using Saiketsu.Gateway.Application.Elections.Queries.GetElectionsForUser;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Domain.Entities;
using Saiketsu.Gateway.Domain.Entities.Election;

namespace Saiketsu.Gateway.Infrastructure.Services;

public sealed class ElectionService : IElectionService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ElectionService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<ElectionEntity>> GetElectionsAsync()
    {
        var httpClient = _httpClientFactory.CreateClient("ElectionClient");

        var response = await httpClient.GetAsync("/api/elections");

        if (response.StatusCode != HttpStatusCode.OK) return new List<ElectionEntity>();

        var stringResponse = await response.Content.ReadAsStringAsync();
        var election = JsonSerializer.Deserialize<List<ElectionEntity>>(stringResponse);

        return election!;
    }

    public async Task<ElectionEntity?> GetElectionAsync(int id)
    {
        var httpClient = _httpClientFactory.CreateClient("ElectionClient");

        var response = await httpClient.GetAsync($"/api/elections/{id}");

        if (response.StatusCode != HttpStatusCode.OK) return null;

        var stringResponse = await response.Content.ReadAsStringAsync();
        var election = JsonSerializer.Deserialize<ElectionEntity>(stringResponse);

        return election!;
    }

    public async Task<bool> DeleteElectionAsync(int id)
    {
        var httpClient = _httpClientFactory.CreateClient("ElectionClient");

        var response = await httpClient.DeleteAsync($"/api/elections/{id}");

        return response.StatusCode == HttpStatusCode.OK;
    }

    public async Task<ElectionEntity?> CreateElectionAsync(CreateElectionCommand command)
    {
        var httpClient = _httpClientFactory.CreateClient("ElectionClient");

        var json = JsonSerializer.Serialize(command);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("/api/elections", content);

        if (response.StatusCode != HttpStatusCode.Created) return null;

        var stringResponse = await response.Content.ReadAsStringAsync();
        var election = JsonSerializer.Deserialize<ElectionEntity>(stringResponse);

        return election!;
    }

    public async Task<bool> AddUserToElectionAsync(AddUserToElectionCommand command)
    {
        var httpClient = _httpClientFactory.CreateClient("ElectionClient");

        var response = await httpClient.PostAsync($"/api/elections/{command.ElectionId}/users/{command.UserId}", null);

        return response.StatusCode == HttpStatusCode.OK;
    }

    public async Task<bool> AddCandidateToElectionAsync(AddCandidateToElectionCommand command)
    {
        var httpClient = _httpClientFactory.CreateClient("ElectionClient");

        var response =
            await httpClient.PostAsync($"/api/elections/{command.ElectionId}/candidates/{command.CandidateId}", null);

        return response.StatusCode == HttpStatusCode.OK;
    }

    public async Task<List<UserEntity>?> GetElectionUsersAsync(int id)
    {
        var httpClient = _httpClientFactory.CreateClient("ElectionClient");

        var response = await httpClient.GetAsync($"/api/elections/{id}/users");

        if (response.StatusCode != HttpStatusCode.OK) return null;

        var stringResponse = await response.Content.ReadAsStringAsync();
        var electionUsers = JsonSerializer.Deserialize<List<UserEntity>>(stringResponse);

        return electionUsers!;
    }

    public async Task<List<CandidateEntity>?> GetElectionCandidatesAsync(int id)
    {
        var httpClient = _httpClientFactory.CreateClient("ElectionClient");

        var response = await httpClient.GetAsync($"/api/elections/{id}/candidates");

        if (response.StatusCode != HttpStatusCode.OK) return null;

        var stringResponse = await response.Content.ReadAsStringAsync();
        var electionCandidates = JsonSerializer.Deserialize<List<CandidateEntity>>(stringResponse);

        return electionCandidates!;
    }

    public async Task<bool> RemoveUserFromElectionAsync(RemoveUserFromElectionCommand command)
    {
        var httpClient = _httpClientFactory.CreateClient("ElectionClient");

        var response = await httpClient.DeleteAsync($"/api/elections/{command.ElectionId}/users/{command.UserId}");

        return response.StatusCode == HttpStatusCode.OK;
    }

    public async Task<bool> RemoveCandidateFromElectionAsync(RemoveCandidateFromElectionCommand command)
    {
        var httpClient = _httpClientFactory.CreateClient("ElectionClient");

        var response =
            await httpClient.DeleteAsync($"/api/elections/{command.ElectionId}/candidates/{command.CandidateId}");

        return response.StatusCode == HttpStatusCode.OK;
    }

    public async Task<List<ElectionEntity>?> GetElectionsForUserAsync(GetElectionsForUserQuery query)
    {
        var httpClient = _httpClientFactory.CreateClient("ElectionClient");

        var queryString = HttpUtility.ParseQueryString(string.Empty);
        queryString["eligible"] = query.Eligible ? "true" : "false";

        var response = await httpClient.GetAsync($"/api/elections/users/{query.UserId}?{queryString}");

        if (response.StatusCode != HttpStatusCode.OK) return null;

        var stringResponse = await response.Content.ReadAsStringAsync();
        var elections = JsonSerializer.Deserialize<List<ElectionEntity>>(stringResponse);

        return elections!;
    }
}