using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleThx.Data
{
    public class Friend
    {

        public int FriendID { get; set; }

        public int AccountID { get; set; }

        public int FreindsUserID { get; set; }

        public Enum Status { get; set; }

        public DateTimeOffset CreateUTC { get; set; }

        public DateTimeOffset? ModifiedUTC { get; set; }




    }
}
