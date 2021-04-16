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

        public Guid FriendReceive { get; set; } // person who receives the request

        public Guid FriendSend { get; set; } // person who sends request

        public FriendStatus Status { get; set; }

      
        public DateTimeOffset CreateUTC { get; set; }
    }
}
