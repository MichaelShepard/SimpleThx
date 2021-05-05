using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SimpleThx.Models
{
    public class AccountInfoCreate
    {

        public Guid UserID { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "First Name is required")]
        [MaxLength(100, ErrorMessage = "Must be less than 100 characters")]
        [RegularExpression(@"^[a-zA-Z]+(\s+[a-zA-Z]+)*$", ErrorMessage = "Use letters only please")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        [MaxLength(100, ErrorMessage = "Must be less than 100 characters")]
        [RegularExpression(@"^[a-zA-Z]+(\s+[a-zA-Z]+)*$", ErrorMessage = "Use letters only please")]
        public string LastName { get; set; }

        [DisplayName("State")]
        [Required(ErrorMessage = "State is required")]
        [MaxLength(2, ErrorMessage = "Please Enter in Two Letters")]
        [RegularExpression(@"^[a-zA-Z]+(\s+[a-zA-Z]+)*$", ErrorMessage = "Use letters only please")]
        public string State { get; set; }

        [DisplayName("Country")]
        [Required(ErrorMessage = "Country is required")]
        [RegularExpression(@"^[a-zA-Z]+(\s+[a-zA-Z]+)*$", ErrorMessage = "Use letters only please")]
        public string Country { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase PictureImage { get; set; }

        [DisplayName("Created")]
        public DateTimeOffset CreateUTC { get; set; }

    }
}
