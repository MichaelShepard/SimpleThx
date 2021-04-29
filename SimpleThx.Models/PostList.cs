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
    public class PostList
    {

        public int PostID { get; set; }

        public Guid PostUserID { get; set; }
       
        public Guid AboutUserID { get; set; }
       
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name ="Content")]
        public string Content { get; set; }

        [Display(Name = "Status")]
        public Status Status { get; set; }

        [DisplayFormat(DataFormatString = "{0:y}")]
        [DisplayName("Posted on")]
        public DateTimeOffset CreateUTC { get; set; }

    }
}
