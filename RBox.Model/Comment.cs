using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBox.Model
{
    public class Comment
    {
        public Guid CommentId { get; set; }
        public Guid UserId { get; set; }
        public Guid FileId { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
    }
}
