using SimpleThx.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleThx.Models
{
    public class PostCreate
    {
        [Required]
        public Guid AboutUserID { get; set; }

        [Required]
        public Guid PostUserID { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        [MaxLength(100, ErrorMessage = "There are too many characters in this field.")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        [Display(Name = "Content")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public Status Status { get; set; }

        [Required]
        [Display(Name = "Created")]
        public DateTimeOffset CreateUTC { get; set; }

    }
}
