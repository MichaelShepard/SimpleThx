using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleThx.Data
{


    public class AccountInfo
    {
        [Key]
        public int AccountID { get; set; }

        [Required]
        public Guid UserID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Country { get; set; }

        public string PictureURL { get; set; }

        public DateTimeOffset CreateUTC { get; set; }

        public DateTimeOffset? ModifiedUTC { get; set; }


        // Foreign Key(s)

        //[InverseProperty(nameof(Friend.Sender))]
        //public virtual ICollection<Friend> FriendSender { get; set; }


        //[InverseProperty(nameof(Friend.Receiver))]
        ///public virtual ICollection<Friend> FriendReceiver { get; set; }// Uses code first principles to create dbo.FriendAccountInfo table which is made up of PK's of each

        public virtual ICollection<Friend> Friends { get; set; }

        public virtual ICollection<Post> Posts { get; set; }  // Uses code first principles to create dbo.PostAccountInfo table which is made up of PK's of each


    } // END Account Class
} // END Namespace
