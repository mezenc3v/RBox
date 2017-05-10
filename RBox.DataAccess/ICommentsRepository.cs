using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RBox.Model;

namespace RBox.DataAccess
{
    public interface ICommentsRepository
    {
        Comment Add(Comment comment);
        IEnumerable<Comment> GetComments(Guid fileId);
        void UpdateText(Guid commentId, string text);
        void Delete(Guid commentId);
    }
}
