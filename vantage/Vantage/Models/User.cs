using System.Collections.Generic;
using HotChocolate.Types.Relay;

namespace Vantage.Models
{
    public class User 
    {
        [ID] public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}