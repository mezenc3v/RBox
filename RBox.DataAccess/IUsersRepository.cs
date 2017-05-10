using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RBox.Model;

namespace RBox.DataAccess
{
    public interface IUsersRepository
    {
        User Add(string name, string login, string password);
        void Delete(Guid id);
        User Get(Guid id);
    }
}
