using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Comments.DTOs;

namespace Shop.Query.Comments.GetByFilter
{
    public class GetCommentByFilterQueryHandler : IQueryHandler<GetCommentByFilterQuery, CommentFilterResult> 
    {
        private readonly ShopContext _context;

        public GetCommentByFilterQueryHandler(ShopContext context)
        {
            _context = context;
        }

        public async Task<CommentFilterResult> Handle(GetCommentByFilterQuery request, CancellationToken cancellationToken)
        {
            var @params = request.FilterParams;

            var result = _context.Comments.OrderByDescending(com => com.CreationDate).AsQueryable();

            if (@params.UserId != null)
                result = result.Where(com => com.UserId == @params.UserId);

            // با کمک این دوتا دستور میتونیم بیایم مشخص کنیم کامنت های بین دو فاصله زمانی دلخواه رو نشونمون بده
            if (@params.StartDate != null)
                result = result.Where(com => com.CreationDate.Date >= @params.StartDate.Value.Date);

            if (@params.EndDate != null)
                result = result.Where(com => com.CreationDate.Date <= @params.EndDate.Value.Date);

            if (@params.CommentStatus != null)
                result = result.Where(com => com.Status == @params.CommentStatus);

            var skip = (@params.PageId) * @params.Take;
            var model = new CommentFilterResult()
            {
                Data = await result.Skip(skip).Take(@params.Take).Select(comment => comment.MapFilterComment()).ToListAsync(cancellationToken),
                FilterParams = @params
                
            };

            model.GeneratePaging(result, @params.Take, @params.PageId);
            return model;
        }


    }
}
