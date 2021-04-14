using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleThx.Data
{


    public class AccountInfo
    {
        [Key]
        public int AccountID { get; set; }

        [Required]
        public Guid UserID { get; set; }

        [Required]
        public  string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Country { get; set; }

        public string PictureURL { get; set; }

        public DateTimeOffset CreateUTC { get; set; }

        public DateTimeOffset? ModifiedUTC { get; set; }

        // Foreign Key(s)

        

    } // END Account Class
} // END Namespace
