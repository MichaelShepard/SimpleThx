using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleThx.Data
{
    public enum CommentStatus
    {
        Accept = 1,
        Decline,
        Pending
    }

    public class Comment
    {

        [Key]
        public int CommentID { get; set; }

        [Required]
        public Guid CommentorUserID { get; set; }

        [Required]
        public string CommentContent { get; set; }

        [Required]
        public CommentStatus Status { get; set; }

        [Required]
        public DateTimeOffset CreateUTC { get; set; }

        public DateTimeOffset? ModifiedUTC { get; set; }

        // Foreign Key(s)

        public virtual Post Post { get; set; } // Uses Entity Framework Notation where one post has many comments; uses the PK in each to create the connection

        public virtual ICollection<AccountInfo> Accounts { get; set; } // Uses Entity Framework Notation for a many to many relationship

    } // END Commment CLass
} // END NameSpace
