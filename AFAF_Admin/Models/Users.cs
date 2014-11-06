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

    public class Users
    {
        [Key, Required]
        public int ID { set; get; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessage = "Invalid email format. Example@example.org")]
        public string email { get; set; }

        [Required(ErrorMessage = "The first name field is required.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Use letters only please")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "The last name field is required.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Use letters only please")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "The password field is required.")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required(ErrorMessage="Please check at least one permission.")]
        public string userType { get; set; }
    }

    public class UsersEntities : DbContext
    {
        public DbSet<Users> Users { get; set; }
    }
}