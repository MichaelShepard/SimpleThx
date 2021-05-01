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
    public class CommentList
    {

        public int CommentID { get; set; }

        public int PostID { get; set; }

        public Guid CommentorUserID { get; set; }

        [DisplayName("Comment")]
        public string CommentContent { get; set; }

       
        public CommentStatus Status { get; set; }

        [DisplayFormat(DataFormatString = "{0:y}")]
        [DisplayName("Commented On")]
        public DateTimeOffset CreateUTC { get; set; }


        [Display(Name = "Post Title")]
        public string Title { get; set; }
    }
}
