using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace AFAF_Admin.Models
{
    public class Recipient
    {
        [Key, Required(ErrorMessage="The user name field is required.")]
        public String userName { get; set; }

        [Required(ErrorMessage = "The first name field is required.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Use letters only please")]
        public String firstName { get; set; }

        [Required(ErrorMessage = "The last name field is required.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Use letters only please")]
        public String lastName { get; set; }

        public String suffix { get; set; }

        [DataType(DataType.ImageUrl)]
        public String imageURL { get; set; }

        [Required(ErrorMessage = "The status field is required.")]
        public bool active { get; set; }    //True is active and False is inactive
    }
}