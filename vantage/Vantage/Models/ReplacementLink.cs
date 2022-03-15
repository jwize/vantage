using HotChocolate;
using HotChocolate.Types.Relay;

namespace Vantage.Models
{
    [GraphQLDescription("Represents replacement hyperlinks for keyword replacement")]
    public class ReplacementLink
    {
        [ID] public int Id { get; set; }
        public string Keyword { get; set; }
        public string Hyperlink { get; set; }
    }
}
