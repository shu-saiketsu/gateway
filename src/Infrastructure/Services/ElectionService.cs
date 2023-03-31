using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Saiketsu.Gateway.Application.Elections.Commands.AddUserToElection;
using Saiketsu.Gateway.Application.Elections.Commands.CreateElection;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Domain.Entities;
using Saiketsu.Gateway.Domain.Entities.Election;

namespace Saiketsu.Gateway.Infrastructure.Services
{
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

            var json = JsonSerializer.Serialize(command);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("/api/elections/add-user", content);

            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
