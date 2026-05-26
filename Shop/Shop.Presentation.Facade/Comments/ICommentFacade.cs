using Common.Application;
using Shop.Application.Comments.ChangeStatus;
using Shop.Application.Comments.Create;
using Shop.Application.Comments.Edit;
using Shop.Query.Comments.DTOs;

namespace Shop.Presentation.Facade.Comments
{
    public interface ICommentFacade
    {
        // Commands //
        Task<OperationResult> ChangeStatusComment(ChangeCommentStatusCommand command);
        Task<OperationResult> CreateComment(CreateCommentCommand command);
        Task<OperationResult> EditComment(EditCommentCommand command);


        // Queries //
        Task<CommentFilterResult> GetCommentsByFilter(CommentFilterParams filterParams);
        Task<CommentDto?> GetCommentById(long id);
    }
}
