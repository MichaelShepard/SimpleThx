using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleThx.Data
{
    public class Account
    {
        [Key]
        public int AccountID { get; set; }

        public int UserID { get; set; }

        [Required]
        public  string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public Enum State { get; set; }

        [Required]
        public Enum Country { get; set; }

        public string PictureURL { get; set; }

        public DateTimeOffset CreateUTC { get; set; }

        public DateTimeOffset? ModifiedUTC { get; set; }

    }
}
