using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace AFAF_Admin.Models
{
    public class Event
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage="The event type field is required.")]
        public String eventType { get; set; }

        [Required(ErrorMessage="The event status field is required.")]
        public bool eventStatus { get; set; }    //True is active and False is inactive

        [Required(ErrorMessage="The event title field is required.")]
        public String eventTitle { get; set; }

        [Required(ErrorMessage="The event date field is required.")]
        [DataType(DataType.Date, ErrorMessage = "Format must be mm/dd/yy or mm/dd/yyyy")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:mm/dd/yyyy}")]
        public DateTime eventDate { get; set; }

        [Required(ErrorMessage="The event time field is required.")]
        [DataType(DataType.Time)]
        [RegularExpression(@"^\s*(?:0?[0-9]|1[0-2]):[0-5][0-9]\s*[AaPp][Mm]\s*$", ErrorMessage="Format: 1:00 am | 1:00 pm | 12:00 am | 12:00 pm ")]
        public String eventTime { get; set; }

        [Required(ErrorMessage = "The event registration close date field is required.")]
        [DataType(DataType.Date, ErrorMessage = "Format must be mm/dd/yy or mm/dd/yyyy")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:mm/dd/yyyy}")]
        public DateTime eventRegCloseDate { get; set; }

        [Required(ErrorMessage="The event location name is required.")]
        public String eventLocationName { get; set; }

        [Required(ErrorMessage="The event address field is required.")]
        public String eventAddress { get; set; }

        [Required(ErrorMessage="The event city field is required.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Use letters only please.")]
        public String eventCity { get; set; }

        [Required(ErrorMessage="The event state is required.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Use letters only please.")]
        public String eventState { get; set; }

        [Required(ErrorMessage="The zip code field is required.")]
        [RegularExpression(@"^[0-9]{5,5}$", ErrorMessage="Invalid zip code, must have 5 digits.")]
        public String eventZip { get; set; }

        [Required(ErrorMessage="The user name field is required.")]
        public String userName { get; set; }

        [Required(ErrorMessage="the price1 field is required.")]
        [DataType(DataType.Currency, ErrorMessage="Only whole numbers is allowed.")]
        public int price1 { get; set; }

        [Required(ErrorMessage="The price2 field is required.")]
        [DataType(DataType.Currency,ErrorMessage="Only whole numbers is allowed.")]
        public int price2 { get; set; }

        [Required(ErrorMessage="The event director field is required.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Use letters only please.")]
        public String eventDirector { get; set; }

        [Required(ErrorMessage="The event director email field is required.")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\s*([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})\s*$", ErrorMessage = "Invalid email format. Example@example.org")]
        public String eventDirectorEmail { get; set; }

        [Required(ErrorMessage="The event director phone number field is required.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9]{10,10}$", ErrorMessage = "Invalid phone number, must have 10 digits without - or ().")]
        public long eventDirectorPhone { get; set; }

        [Required(ErrorMessage="The event max capacity field is required.")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only whole numbers is allowed.")]
        public int eventMaxCapacity { get; set; }

        [AllowHtml]
        [Required(ErrorMessage="The event desscription field is required.")]
        public String eventDescription { get; set; }

        [AllowHtml]
        [Required(ErrorMessage="The event details field is required.")]
        public String eventDetails { get; set; }

        [Required(ErrorMessage="The auction lead name field is required.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Use letters only please.")]
        public String auctionLeadName { get; set; }
    }
}