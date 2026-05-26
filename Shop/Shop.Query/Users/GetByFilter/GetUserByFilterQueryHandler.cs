using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.GetByFilter
{
    public class GetUserByFilterQueryHandler : IQueryHandler<GetUserByFilterQuery, UserFilterResult>
    {
        private readonly ShopContext _shopContext;

        public GetUserByFilterQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<UserFilterResult> Handle(GetUserByFilterQuery request, CancellationToken cancellationToken)
        {
            var @params = request.FilterParams;
            var result = _shopContext.Users.OrderByDescending(u => u.Id).AsQueryable();

            if (@params.Id != null)
                result = result.Where(u => u.Id == @params.Id);

            if (!string.IsNullOrWhiteSpace(@params.PhoneNumber))
                result = result.Where(u => u.PhoneNumber.Contains(@params.PhoneNumber));

            if (!string.IsNullOrWhiteSpace(@params.Email))
                result = result.Where(u => u.Email.Contains(@params.Email));

            var skip = (@params.PageId - 1) * @params.Take;
            var model = new UserFilterResult()
            {
                Data = await result.Skip(skip).Take(@params.Take).Select(u => u.MapFilterData()).ToListAsync(cancellationToken),
                FilterParams = @params
            };

            model.GeneratePaging(result, @params.Take, @params.PageId);

            return model;
        }

    }

}
