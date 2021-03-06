using SimpleThx.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleThx.Models
{
    public class FriendList
    {
        [Display(Name = "Friend ID")]
        public int FriendID { get; set; }

        public Guid UserID { get; set; }

        public int AccountID { get; set; }

        [Display(Name = "Name")]
        public string FullName { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public Guid FriendReceive { get; set; } // person who receives the request

        public Guid FriendSend { get; set; } // person who sends request

    
        public FriendStatus Status { get; set; }

        
        [DisplayFormat(DataFormatString = "{0:y}")]
        [DisplayName("Friends Since")]
        public DateTimeOffset CreateUTC { get; set; }

    }
}
