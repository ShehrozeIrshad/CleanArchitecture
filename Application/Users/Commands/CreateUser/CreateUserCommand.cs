using Application.Abstractions;
using Domain.Shared;

namespace Application.Users.Commands.CreateUser
{
    public sealed record CreateUserCommand(Guid id, string email, string password, string firstName, string lastName, string? ipAddress, string? device, decimal balance) :  ICommand<Result<Guid>>;
}
