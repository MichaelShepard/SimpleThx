using SimpleThx.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleThx.Models
{
    public class FriendCreate
    {

        public Guid AccountID { get; set; }

      
        public FriendStatus Status { get; set; }

      
        public DateTimeOffset CreateUTC { get; set; }
    }
}
