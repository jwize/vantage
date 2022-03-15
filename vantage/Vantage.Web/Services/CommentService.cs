using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Vantage.Web.Models;

namespace Vantage.Web.Services
{
    public class CommentService
    {
        public ImmutableList<Comment> Comments { get; set; }

        public Comment Project(IOnAddedComment_Created comment)
        {
            var added = new Comment
            {
                Id = comment.Id,
                Content = comment?.Content,
                User = new User
                {
                    Id = comment?.UserId ?? 0,
                    Name = comment.User?.Name,
                    Direction = comment.User?.Name == "Jaime" ? Direction.Left : Direction.Right
                }
            };
            return added;
        }

        public void Update(IReadOnlyList<IGetComments_Comment> data)
        {
            if (data == null) return;
            var comments = data.Select(c => new Comment
            {
                Id = c.Id,
                Content = c.Content,
                User = new User
                {
                    Id = c.UserId,
                    Direction = c.User?.Name == "Jaime" ? Direction.Left : Direction.Right,
                    Name = c.User?.Name
                }
            });
            Comments = comments.ToImmutableList();
        }
    }
}