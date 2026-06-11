using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Application.Comments.ChangeStatus;
using Shop.Application.Comments.Create;
using Shop.Application.Comments.Edit;
using Shop.Domain.RoleAggregate.Enums;
using Shop.Presentation.Facade.Comments;
using Shop.Query.Comments.DTOs;
using System.Runtime.CompilerServices;

namespace Shop.Api.Controllers
{
    public class CommentController : ApiController
    {
        private readonly ICommentFacade _commentFacade;

        public CommentController(ICommentFacade commentFacade)
        {
            _commentFacade = commentFacade;
        }

        [PermissionChecker(Permission.Comment_Management)]
        [HttpGet("{commentId}")]
        public async Task<ApiResult<CommentDto?>> GetCommentById(long commentId)
        {
            var comment = await _commentFacade.GetCommentById(commentId);
            return QueryResult(comment);
        }

        [PermissionChecker(Permission.Comment_Management)]
        [HttpGet]
        public async Task<ApiResult<CommentFilterResult>> GetCommentByFilter([FromQuery]CommentFilterParams filterParams)
        {
            var comment = await _commentFacade.GetCommentsByFilter(filterParams);
            return QueryResult(comment);
        }

        [HttpPost]
        [Authorize]
        public async Task<ApiResult> CreateComment(CreateCommentCommand command)
        {
            var comment = await _commentFacade.CreateComment(command);
            return CommandResult(comment);
        }

        [HttpPut]
        public async Task<ApiResult> EditComment(EditCommentCommand command)
        {
            var comment = await _commentFacade.EditComment(command);
            return CommandResult(comment);
        }

        
        [PermissionChecker(Permission.Comment_Management)] // یعنی باید ادمین اجازه بده تا بتونه کامنت بذاره
        [HttpPut("changeStatus")]
        public async Task<ApiResult> ChangeStatusComment(ChangeCommentStatusCommand command)
        {
            var comment = await _commentFacade.ChangeStatusComment(command);
            return CommandResult(comment);
        }


    }
}
