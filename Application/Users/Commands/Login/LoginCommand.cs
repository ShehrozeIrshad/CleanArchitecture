using Application.Abstractions;
using Domain.Shared;

namespace Application.Users.Commands.Login
{
    public record LoginCommand(string email) : ICommand<Result<string>>;
}
