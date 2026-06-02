using Common.Query;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.GetUserToken
{
    public record GetUserTokenByRefreshTokenQuery(string HashRefreshToken) : IQuery<UserTokenDto?>;
}
