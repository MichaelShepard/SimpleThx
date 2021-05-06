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
    public class PostCommentList
    {
        public int PostID { get; set; }

        public Guid UserID { get; set; }

        public int AccountID { get; set; }

        [Display(Name = "Name")]
        public string FullName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public Guid PostUserID { get; set; }

        public Guid AboutUserID { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Content")]
        public string Content { get; set; }

        [Display(Name = "Status")]
        public Status Status { get; set; }


        public int CommentID { get; set; }

        public Guid CommentorUserID { get; set; }

        [DisplayName("Comment")]
        public string CommentContent { get; set; }


        [DisplayFormat(DataFormatString = "{0:y}")]
        [DisplayName("Posted on")]
        public DateTimeOffset CreateUTC { get; set; }
    }
}
