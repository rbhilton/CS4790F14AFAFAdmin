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
    public class Runners
    {
        [Key, Required]
        public int Pkey { get; set; }

        [Required]
        public int transID { get; set; }

        [Required]
        public int participantID { get; set; }

        [Required]
        public int eventID { get; set; }

        [Required]
        public int runnerNo { get; set; }

        [Required]
        public String firstName { get; set; }

        [Required]
        public String lastName { get; set; }

        [Required]
        public String shirtSize { get; set; }
    }
}