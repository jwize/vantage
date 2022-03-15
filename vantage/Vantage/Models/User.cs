using System.Collections.Generic;
using HotChocolate.Types;

namespace Vantage.Models
{
    public class User 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}