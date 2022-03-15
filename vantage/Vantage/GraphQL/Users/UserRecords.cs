using Vantage.Models;

namespace Vantage.GraphQL.Users
{
    public record UserRecords(string Name);
    public record AddUserPayload(User user);
}