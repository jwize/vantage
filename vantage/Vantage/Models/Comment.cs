using System;
using HotChocolate;
using HotChocolate.Types.Relay;

namespace Vantage.Models
{
    [GraphQLDescription("Represents comments that are authored by users in the communication.")]
    public class Comment
    {
        [ID] public int Id { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public User User { get; set; }
        [ID] public int UserId { get; set; }
    }
}