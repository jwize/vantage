using Vantage.Models;

namespace Vantage.GraphQL.Comments
{
    public record CommentRecords(string Content, int UserId);
    public record AddCommentPayload(Comment comment);
}