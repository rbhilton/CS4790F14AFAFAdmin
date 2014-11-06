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
    public class EventInstance
    {
        [Key, Required]
        public int eventInstanceID { get; set; }

        [Required]
        public int eventID { get; set; }

        [Required]
        public int participantID { get; set; }

        [Required]
        public int transID { get; set; }

        [Required]
        public DateTime transDate { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public long retailValue { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public long cashValue { get; set; }

        [Required]
        public String donationType { get; set; }

        [Required]
        public bool anonymous { get; set; }

        [Required]
        public String tshirtSize { get; set; }

        [Required]
        public String ticketType { get; set; }

        [Required]
        public int numOfChildren { get; set; }

        [Required]
        public int numOfAdults { get; set; }

        [Required]
        public int numInParty { get; set; }

        [Required]
        public String receiptStatus { get; set; }

        [Required]
        public String runTeamName { get; set; }

        [Required]
        public String runTeamColor { get; set; }
    }
}