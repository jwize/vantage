using Vantage.Models;

namespace Vantage.GraphQL.Users
{
    public record AddReplacementLinkInput(string Keyword, string Hyperlink);
    public record AddReplacementLinkPayload(ReplacementLink ReplacementLink);
    public record RemoveReplacementLinkPayload (ReplacementLink ReplacementLink);

}