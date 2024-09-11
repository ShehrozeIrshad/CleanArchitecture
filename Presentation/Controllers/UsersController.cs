using Application.Users.Commands.CreateUser;
using Application.Users.Commands.Login;
using Application.Users.Queries.GetBalance;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;
using Presentation.Contracts.Users;

namespace Presentation.Controllers
{
    [Route("api/users")]
    public sealed class UsersController : ApiController
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sender"></param>
        public UsersController(ISender sender) : base(sender) { }

        /// <summary>
        /// Register the user
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        [HttpPost("signup")]
        public async Task<IActionResult> RegisterUser(
        [FromBody] RegisterUserRequest request,
        CancellationToken cancellationToken)
        {

            var command = new CreateUserCommand(
                Guid.NewGuid(),
                request.email,
                request.password,
                request.firstName,
                request.lastName,
                "127.0.0.1",
                "Browser",
                5);

            Result<Guid> result = await Sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Authenticate user via Email
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        [HttpPost("authenticate")]
        public async Task<IActionResult> LoginMember(
        [FromBody] LogInRequest request,
        CancellationToken cancellationToken)
        {
            var command = new LoginCommand(request.Email);

            Result<string> tokenResult = await Sender.Send(
                command,
                cancellationToken);

            if (tokenResult.IsFailure)
            {
                return HandleFailure(tokenResult);
            }

            return Ok(tokenResult.Value);
        }

        /// <summary>
        /// Get User Balance
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        [Authorize]
        [HttpGet("getBalance/{id:guid}")]
        public async Task<IActionResult> GetBalance(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetBalanceQuery(id);

            Result<decimal> response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
        }

    }
}
