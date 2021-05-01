using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleThx.Models
{
    public class PostEdit
    {

        public int PostID { get; set; }

        public Guid PostUserID { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Content")]
        public string Content { get; set; }

        [DisplayFormat(DataFormatString = "{0:y}")]
        [DisplayName("Updated on")]
        public DateTimeOffset? ModifiedUTC { get; set; }
    }
}
