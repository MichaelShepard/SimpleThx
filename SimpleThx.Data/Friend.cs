using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleThx.Data
{

    public enum FriendStatus
    {
        Accept = 1 ,
        Decline
    }


    public class Friend
    {

        [Key]
        public int FriendID { get; set; }

        [Required]
        public Guid AccountID { get; set; }

        [Required]
        public FriendStatus Status { get; set; }

        [Required]
        public DateTimeOffset CreateUTC { get; set; }

        public DateTimeOffset? ModifiedUTC { get; set; }


        // Foreign Key(s)

        

        public int FreindsUserID { get; set; }

    } // END Friend Class
} // END Namespace
