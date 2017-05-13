using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RBox.Model;

namespace RBox.Data
{
    public interface ICommentsRepository
    {
        Comment AddComment(Comment comment);
        IEnumerable<Comment> GetComments(Guid fileId);
        void UpdateComment(Guid commentId, Comment comment);
        void DeleteComment(Guid commentId);
    }
}
