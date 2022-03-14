using HotChocolate;
using HotChocolate.Types;
using Vantage.Models;

namespace Vantage.GraphQL
{
    public class Subscription
    {
        [Subscribe,Topic]
        public Comment OnCommentCreated([EventMessage] Comment comment) => comment;
        [Subscribe, Topic]
        public ReplacementLink OnReplacementLinkCreated([EventMessage] ReplacementLink replacementLink) => replacementLink;
        [Subscribe, Topic]
        public ReplacementLink OnReplacementLinkRemoved([EventMessage] ReplacementLink replacementLink) => replacementLink;
    }
}
