using Common.Application;
using Shop.Application.Comments.Edit;
using Shop.Domain.CommentAggregate.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Comments.ChangeStatus
{
    public record ChangeCommentStatusCommand(long Id, CommentStatus Status) : IBaseCommand;
}
