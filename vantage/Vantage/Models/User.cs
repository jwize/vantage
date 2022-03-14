using System.Collections.Generic;

namespace Vantage.Models
{

    public class User 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}