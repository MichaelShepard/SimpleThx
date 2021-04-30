using SimpleThx.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleThx.Models
{
    public class CommentCreate
    {
        public int CommentID { get; set; }

        public int PostID { get; set; }

        public Guid CommentorUserID { get; set; }

        [Required]
        [MaxLength(1000)]
        [Display(Name = "Comment")]
        public string CommentContent { get; set; }

        public CommentStatus Status { get; set; }

        [Required]
        [Display(Name = "Created")]
        public DateTimeOffset CreateUTC { get; set; }
    }
}
