using System.Linq;
using HotChocolate;
using Vantage.Models;

namespace Vantage.GraphQL
{
    public class Resolvers
    {
        public IQueryable<Comment> GetComments([Parent] User user, [ScopedService] Database database)
        {
            return database.Comments.Where(c => c.UserId == user.Id);
        }
        public User GetUser([Parent] Comment comment, [ScopedService] Database database)
        {
            return database.Users.Find(comment.UserId);
        }
    }
}
