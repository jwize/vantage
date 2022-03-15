using Vantage.Models;

namespace Vantage.GraphQL.Users
{
    public record AddUserInput(string Name);
    public record AddUserPayload(User user);
}