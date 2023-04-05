using Saiketsu.Gateway.Application.Users.Commands.BlockUser;
using Saiketsu.Gateway.Application.Users.Commands.CreateUser;
using Saiketsu.Gateway.Application.Users.Commands.UnblockUser;
using Saiketsu.Gateway.Domain.Entities;

namespace Saiketsu.Gateway.Application.Interfaces;

public interface IUserService
{
    Task<UserEntity?> CreateUserAsync(CreateUserCommand command);
    Task<List<UserEntity>> GetUsersAsync();
    Task<UserEntity?> GetUserAsync(string id);
    Task<bool> DeleteUserAsync(string id);
    Task<bool> BlockUserAsync(BlockUserCommand command);
    Task<bool> UnblockUserAsync(UnblockUserCommand command);
}