using HotChocolate.Types.Relay;
using Vantage.Models;

namespace Vantage.GraphQL.Comments
{
    public record AddCommentInput(string Content, [ID]int UserId);
    public record AddCommentPayload(Comment comment);
}