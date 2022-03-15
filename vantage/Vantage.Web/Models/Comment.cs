using System;
namespace Vantage.Web.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public User User { get; set; }
        


    }
}