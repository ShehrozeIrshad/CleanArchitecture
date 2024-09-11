
using Domain.Shared;

namespace Domain.Errors
{
    public static class DomainErrors
    {
        public static class User
        {
            public static readonly Error EmailAlreadyInUse = new(
                "User.EmailAlreadyInUse",
                "The specified email is already in use");

            public static readonly Error NotFound = new Error(
                "User.NotFound",
                $"User was not found.");

            public static readonly Error InvalidCredentials = new(
                "User.InvalidCredentials",
                "The provided credentials are invalid");
        }
    }
}
