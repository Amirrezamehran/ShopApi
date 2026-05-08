using Shop.Domain.CommentAggregate;
using Shop.Domain.CommentAggregate.Repository;
using Shop.Infrastructure.Persistent.Ef._Utilities;

namespace Shop.Infrastructure.Persistent.Ef.CommentAggregate
{
    internal class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(ShopContext context) : base(context)
        {
        }
    }
}
