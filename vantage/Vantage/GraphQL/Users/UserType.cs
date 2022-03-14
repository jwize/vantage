using System.Linq;
using HotChocolate;
using HotChocolate.Types;
using Vantage.Models;

namespace Vantage.GraphQL.Users
{
    public class UserType : ObjectType<User>
    {
        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
            descriptor.Description("Represents users in the system that can take part in the communications.");
            descriptor.Field<User>(u => u.Id).Description("Represents id for user lookup.");
            descriptor.Field<User>(u => u.Name).Description("Represents the name of the user taking part in the communication.");

            descriptor.Field(c => c.Comments)
                .ResolveWith<Resolvers>(c => c.GetComments(default!, default!))
                .UseDbContext<Database>()
                .Description("Represents a list of comments authored by the user");
        }

        private class Resolvers
        {
            public IQueryable<Comment> GetComments([Parent] User user, [ScopedService] Database database)
            {
                return database.Comments.Where(c => c.UserId == user.Id);
            }
        }
    }
}
