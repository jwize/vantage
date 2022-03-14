using Vantage.Models;

namespace Vantage.GraphQL.Users
{
    public record AddReplacementLinkInput(string Keyword, string Hyperlink);
    public record ReplacementRecords(ReplacementLink ReplacementLink);
    public record RemoveReplacementLinkPayload (ReplacementLink ReplacementLink);

}