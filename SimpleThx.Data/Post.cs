using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleThx.Data
{

    public enum Status
    {
        Accepted = 1,
        Declined,
        Pending
    }


    public class Post
    {

        [Key]
        public int PostID { get; set; }

        [Required]
        public Guid PostUserID { get; set; }

        [Required]
        public Guid AboutUserID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public DateTimeOffset CreateUTC { get; set; }

        public DateTimeOffset? ModifiedUTC { get; set; }


        // Foreign Key(s)

        public virtual ICollection<AccountInfo> Accounts { get; set; } // Uses code first principles to create dbo.PostAccountInfo table which is made up of PK's of each

        public virtual ICollection<Comment> Comments { get; set; }


    } // END Post Class
 }  // END Namespace
