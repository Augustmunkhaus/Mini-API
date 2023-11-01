using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared.Model
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Upvotes { get; set; } = 0;
        public int Downvotes { get; set; } = 0;
        public DateTime Date { get; set; } = DateTime.Now;
        public User User { get; set; }
        public Comment(string content = "", User user = null)
        {
            Content = content;
            
            User = user;
        }
        public Comment()
        {
            Id = 0;
            Content = "";
            Upvotes = 0;
            Downvotes = 0;
        }
    }
}