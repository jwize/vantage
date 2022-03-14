using System.Linq;
using HotChocolate;
using HotChocolate.Data;
using Vantage.Models;

namespace Vantage.GraphQL
{
    [GraphQLDescription("This represents the API for a linked text messaging application.")]
    public class Query
    {
        [UseDbContext(typeof(Database)), UseFiltering, UseSorting]
        public IQueryable<User> GetUser([ScopedService]Database database)
        {
            return database.Users;
        }
        [UseDbContext(typeof(Database)), UseFiltering, UseSorting]
        public IQueryable<Comment> GetComment([ScopedService] Database database)
        {
            return database.Comments;
        }

        [UseDbContext(typeof(Database)), UseFiltering, UseSorting]
        public IQueryable<ReplacementLink> GetReplacementLink([ScopedService] Database database)
        {
            return database.ReplacementLinks;
        }
    }
}