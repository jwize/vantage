using System.Linq;
using HotChocolate;
using HotChocolate.Types;
using Vantage.Models;

namespace Vantage.GraphQL.Users
{
    public class ReplacementLinkType : ObjectType<ReplacementLink>
    {
        protected override void Configure(IObjectTypeDescriptor<ReplacementLink> descriptor)
        {
            descriptor.Description("Represents replacement link to replace value of comment with.");
            descriptor.Field<ReplacementLink>(u => u.Id).Description("Represents id for user lookup.");
            descriptor.Field<ReplacementLink>(u => u.Keyword).Description("Represents the keyword for searching hyperlinks in comment text.");
            descriptor.Field<ReplacementLink>(u => u.Hyperlink).Description("Represents the hyperlink that will wrap the keyword when a keyword match is found in comment text.");
        }
    }
}
