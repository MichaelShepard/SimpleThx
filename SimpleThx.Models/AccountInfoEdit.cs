using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleThx.Models
{
    public class AccountInfoEdit
    {

        public int AccountID { get; set; }


        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [MaxLength(2, ErrorMessage = "Please Enter in Two Letters")]
        public string State { get; set; }

        public string Country { get; set; }

        [DisplayName ("Modified")]
        public DateTimeOffset? ModifiedUTC { get; set; }

    }
}
