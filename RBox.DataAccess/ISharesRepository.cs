using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RBox.Model;

namespace RBox.DataAccess
{
    public interface ISharesRepository
    {
        Share GetShare(Guid id);
        Share Add(Share share);
        void Delete(Guid id);
        IEnumerable<Share> GetUserShares(Guid userId);
    }
}
