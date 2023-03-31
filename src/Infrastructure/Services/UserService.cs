using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Application.Users.Commands.CreateUser;
using Saiketsu.Gateway.Domain.Entities;

namespace Saiketsu.Gateway.Infrastructure.Services
{
    public sealed class UserService : IUserService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<UserEntity?> CreateUserAsync(CreateUserCommand command)
        {
            var httpClient = _httpClientFactory.CreateClient("UserClient");

            var json = JsonSerializer.Serialize(command);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("/api/users", content);

            if (response.StatusCode != HttpStatusCode.OK) return null;

            var stringResponse = await response.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<UserEntity>(stringResponse);

            return user!;
        }

        public async Task<List<UserEntity>> GetUsersAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("UserClient");

            var response = await httpClient.GetAsync("/api/users");

            var stringResponse = await response.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<List<UserEntity>>(stringResponse);

            return users!;
        }

        public async Task<UserEntity?> GetUserAsync(string id)
        {
            var httpClient = _httpClientFactory.CreateClient("UserClient");

            var response = await httpClient.GetAsync($"/api/users/{id}");

            if (response.StatusCode != HttpStatusCode.OK) return null;

            var stringResponse = await response.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<UserEntity>(stringResponse);

            return users!;
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var httpClient = _httpClientFactory.CreateClient("UserClient");

            var response = await httpClient.DeleteAsync($"/api/users/{id}");

            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
