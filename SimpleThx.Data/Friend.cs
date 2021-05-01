using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleThx.Data
{

    public enum FriendStatus
    {
        Accepted = 1 ,
        Declined,
        Pending
        
    }

    public class Friend
    {

        [Key]
        public int FriendID { get; set; }

        [Required]
        public Guid FriendReceive { get; set; } // person who receives the request

        [Required]
        public Guid FriendSend { get; set; } // person who sends request

        [Required]
        public FriendStatus Status { get; set; }

        [Required]
        public DateTimeOffset CreateUTC { get; set; }

        public DateTimeOffset? ModifiedUTC { get; set; }


        // Foreign Key(s)

       
        public virtual ICollection<AccountInfo> Accounts { get; set; }

    } // END Friend Class
} // END Namespace
