using System;
using Vantage.Models;

namespace Vantage.GraphQL.Comments
{
    public record AddCommentInput(string Content, Int32 UserId);
}