using HotChocolate;
using HotChocolate.Types;
using Vantage.Models;

namespace Vantage.GraphQL.Comments
{
    public class CommentType : ObjectType<Comment>
    {
        protected override void Configure(IObjectTypeDescriptor<Comment> descriptor)
        {
            descriptor.Description("Represents a comment authored by a user");
            
            descriptor.Field(c => c.User)
                .ResolveWith<Resolvers>(r => r.GetUser(default!, default!))
                .UseDbContext<Database>()
                .Description("This is the user to which the comment belongs");
        }

        private class Resolvers
        {
            public User GetUser([Parent] Comment comment, [ScopedService] Database database)
            {
                return database.Users.Find(comment.UserId);
            }
        }
    }
}
