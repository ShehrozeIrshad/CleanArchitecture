using Application.Abstractions;
using Dapper;
using Domain.Shared;
using Microsoft.Data.SqlClient;

namespace Application.Users.Commands.CreateUser
{
    internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Result<Guid>>
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public CreateUserCommandHandler(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await using SqlConnection sqlConnection = _connectionFactory.CreateConnection();

            sqlConnection.Open();

            var insertQuery = @"INSERT INTO 
                        Users 
                        (Id, Email, Password, FirstName, LastName, Device, IpAddress, Balance)
                        OUTPUT INSERTED.Id
                        VALUES 
                        (@id, @email, @password, @firstName, @lastName, @device, @ipAddress, @balance)";


           var userId = await sqlConnection.QuerySingleAsync<Guid>(insertQuery, 
                        new 
                        { 
                            request.id,
                            request.email, 
                            request.password, 
                            request.firstName, 
                            request.lastName, 
                            request.device, 
                            request.ipAddress,
                            request.balance 
                        }
                        );

            sqlConnection.Close();


            return Result.Success(userId);
        }
    }
}
