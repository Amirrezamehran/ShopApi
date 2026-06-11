using Common.Query;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.GetUserToken.GetByJwtToken
{
    public record GetUserTokenByJwtTokenQuery(string HashJwtToken) : IQuery<UserTokenDto?>;
}
