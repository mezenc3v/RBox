using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBox.Model
{
    public class User
    {
        public Guid UserId { get; set; }
        public string UserLogin { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
