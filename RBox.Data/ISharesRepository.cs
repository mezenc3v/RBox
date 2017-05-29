using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RBox.Model;

namespace RBox.Data
{
    public interface ISharesRepository
    {
        Share GetShare(Guid shareId);
        Share AddShare(Share share);
        void DeleteShare(Guid shareId);
        IEnumerable<Share> GetUserShares(Guid userId);
    }
}
