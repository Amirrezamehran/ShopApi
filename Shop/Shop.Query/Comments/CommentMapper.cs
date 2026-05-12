using Shop.Domain.CommentAggregate;
using Shop.Query.Comments.DTOs;

namespace Shop.Query.Comments
{
    public static class CommentMapper
    {
        public static CommentDto? Map(this Comment? comment)
        {
            if (comment == null)
                return null;

            return new CommentDto()
            {
                Id = comment.Id,
                UserId = comment.UserId,
                ProductId = comment.ProductId,
                Text = comment.Text,
                Status = comment.Status,
                CreationDate = comment.CreationDate
            };
        }

        public static CommentDto MapFilterComment(this Comment comment)
        {
            if (comment == null)
                return null;

            return new CommentDto()
            {
                Id = comment.Id,
                UserId = comment.UserId,
                ProductId = comment.ProductId,
                Text = comment.Text,
                Status = comment.Status,
                CreationDate = comment.CreationDate
            };
        }

    }

} // End Class
