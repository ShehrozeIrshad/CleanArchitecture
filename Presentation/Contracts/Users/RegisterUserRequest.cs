namespace Presentation.Contracts.Users
{
    public sealed record RegisterUserRequest(
     string email,
     string password,
     string firstName,
     string lastName);
}
