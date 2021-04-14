using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleThx.Models
{
    public class PostList
    {

        public int PostID { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name ="Content")]
        public string Content { get; set; }

        public DateTimeOffset CreateUTC { get; set; }

    }
}
