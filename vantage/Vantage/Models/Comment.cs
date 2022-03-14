using System;
using HotChocolate;

namespace Vantage.Models
{
    [GraphQLDescription("Represents comments that are authored by users in the communication.")]
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}