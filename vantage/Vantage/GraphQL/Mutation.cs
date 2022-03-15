using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Vantage.GraphQL.Comments;
using Vantage.GraphQL.Users;
using Vantage.Models;

namespace Vantage.GraphQL
{
    public class Mutation
    {
        [UseDbContext(typeof(Database))]
        public async Task<AddUserPayload> AddUserAsync(AddUserInput input, [ScopedService] Database database)
        {
            var user = new User
            {
                Name = input.Name
            };
            database.Users.Add(user);
            await database.SaveChangesAsync();
            return new AddUserPayload(user);
        }

        [UseDbContext(typeof(Database))]
        public async Task<AddCommentPayload> AddCommentAsync(AddCommentInput input, [ScopedService] Database database
        ,[Service] ITopicEventSender eventSender, CancellationToken cancellationToken)
        {
            var comment = new Comment
            {
                Content = input.Content,
                UserId = input.UserId,
            };

            database.Add(comment);
            await database.SaveChangesAsync(cancellationToken);

            await eventSender.SendAsync(nameof(Subscription.OnCommentCreated), comment, cancellationToken);

            return new AddCommentPayload(comment);
        }

        [UseDbContext(typeof(Database))]
        public async Task<ReplacementRecords> AddReplacementLinkAsync(AddReplacementLinkInput input,
            [ScopedService] Database database, [Service] ITopicEventSender eventSender, CancellationToken cancellationToken)
        {
            var link = new ReplacementLink
            {
                Hyperlink = input.Hyperlink,
                Keyword = input.Keyword
            };

            database.Add(link);
            await database.SaveChangesAsync(cancellationToken);

            await eventSender.SendAsync(nameof(Subscription.OnReplacementLinkCreated), link, cancellationToken);

            return new ReplacementRecords(link);
        }

        [UseDbContext(typeof(Database))]
        public async Task<RemoveReplacementLinkPayload> RemoveReplacementLinkAsync(string keyword,
            [ScopedService] Database database, [Service] ITopicEventSender eventSender, CancellationToken cancellationToken)
        {
            var removed = await database.ReplacementLinks.FirstOrDefaultAsync(r => r.Keyword == keyword, cancellationToken);
            database.Remove(removed);
            await database.SaveChangesAsync(cancellationToken);

            await eventSender.SendAsync(nameof(Subscription.OnReplacementLinkRemoved), removed, cancellationToken);

            return new RemoveReplacementLinkPayload(removed);
        }
    }
}


