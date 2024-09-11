using Application.Abstractions;
using Domain.Shared;

namespace Application.Users.Queries.GetBalance
{
    public sealed record GetBalanceQuery(Guid userId) : IQuery<Result<decimal>>;
}
