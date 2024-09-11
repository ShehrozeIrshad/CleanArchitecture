using Application.Abstractions;
using Dapper;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using Microsoft.Data.SqlClient;

namespace Application.Users.Commands.Login
{
    internal sealed class LoginCommandHandler
    : ICommandHandler<LoginCommand, Result<string>>
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly ISqlConnectionFactory _connectionFactory;

        public LoginCommandHandler(IJwtProvider jwtProvider, ISqlConnectionFactory connectionFactory)
        {
            _jwtProvider = jwtProvider;
            _connectionFactory = connectionFactory;
        }

        public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            await using SqlConnection sqlConnection = _connectionFactory.CreateConnection();

            sqlConnection.Open();

            User? user = await sqlConnection.QueryFirstOrDefaultAsync<User>(
                                             @"SELECT Id, Email, FirstName, LastName
                                                FROM USERS
                                                WHERE Email = @email", new { request.email });

            if(user is null)
            {
                return Result.Failure<string>(
               DomainErrors.User.InvalidCredentials);
            }

            string token = _jwtProvider.Generate(user);

            sqlConnection.Close();

            return token;
        }
    }
}
