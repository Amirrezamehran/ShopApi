using Common.Query;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.GetUserToken.GetByRefreshToken
{
    public record GetUserTokenByRefreshTokenQuery(string HashRefreshToken) : IQuery<UserTokenDto?>;
}
