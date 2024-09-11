using Application.Abstractions;
using Dapper;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using Microsoft.Data.SqlClient;

namespace Application.Users.Queries.GetBalance
{
    internal sealed class GetBalanceQueryHandler : IQueryHandler<GetBalanceQuery, Result<decimal>>
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public GetBalanceQueryHandler(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Result<decimal>> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
        {
            await using SqlConnection sqlConnection = _connectionFactory.CreateConnection();

            User? user = await sqlConnection.QueryFirstOrDefaultAsync<User>(
                                             @"SELECT Id, Email, FirstName, LastName
                                                FROM dbo.USERS
                                                WHERE Id = @userId", new { request.userId });

            if (user is null)
            {
                return Result.Failure<decimal>(DomainErrors.User.NotFound);
            }

            var balance = await sqlConnection.QuerySingleAsync<decimal>(@"SELECT Balance
                                                                          FROM dbo.Users
                                                                          WHERE Id = @userId", new {request.userId});

            return Result.Success(balance);
        }
    }
}
