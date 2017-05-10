using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBox.Model
{
    public class Share
    {
        public Guid ShareId { get; set; }
        public Guid UserId { get; set; }
        public Guid FileId { get; set; }
    }
}
