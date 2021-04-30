using SimpleThx.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleThx.Models
{
    public class CommentList
    {

        public int CommentID { get; set; }

        
        public Guid CommentorUserID { get; set; }

       
        public string CommentContent { get; set; }

       
        public CommentStatus Status { get; set; }

        
        public DateTimeOffset CreateUTC { get; set; }
    }
}
