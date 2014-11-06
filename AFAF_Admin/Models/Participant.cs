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
    public class Participant
    {
        [Key, Required]
        public int participantID { get; set; }

        [Required(ErrorMessage = "The first name field is required.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Use letters only please")]
        public String firstName { get; set; }

        [Required(ErrorMessage = "The last name field is required.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Use letters only please")]
        public String lastName { get; set; }

        public String suffix { get; set; }

        [Required(ErrorMessage = "The event director email field is required.")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\s*([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})\s*$", ErrorMessage = "Invalid email format. Example@example.org")]
        public String email { get; set; }

        [Required(ErrorMessage = "The donor phone number field is required.")]
        [RegularExpression(@"^[0-9]{10,10}$", ErrorMessage = "Invalid phone number, must have 10 digits without - or ().")]
        [DataType(DataType.PhoneNumber)]
        public long phone { get; set; }

        [Required(ErrorMessage = "The emergency contact name field is required.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Use letters only please")]
        public String emContactName { get; set; }

        [Required(ErrorMessage = "The emergency contact phone number field is required.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9]{10,10}$", ErrorMessage = "Invalid phone number, must have 10 digits without - or ().")]
        public String emContactPhone { get; set; }
    }
}