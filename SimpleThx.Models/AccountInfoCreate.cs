using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleThx.Models
{
    public class AccountInfoCreate
    {

        public Guid UserID { get; set; }

        [Required (ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "State is required")]
        [MaxLength(2, ErrorMessage = "Please Enter in Two Letters")]
        public string State { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string Country { get; set; }

        public string PictureURL { get; set; }

        [DisplayName("Created")]
        public DateTimeOffset CreateUTC { get; set; }

    }
}
