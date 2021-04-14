using SimpleThx.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleThx.Models
{
    public class FriendList
    {

        public int FriendID { get; set; }

        
        public Guid AccountID { get; set; }

    
        public FriendStatus Status { get; set; }

        [Display(Name="Created")]
        public DateTimeOffset CreateUTC { get; set; }

    }
}
