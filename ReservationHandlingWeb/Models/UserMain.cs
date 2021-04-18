using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservationHandlingWeb.Models
{
    public class UserMain
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Key, ForeignKey("EventType")]
        [Required]
        [DisplayName("Event Type")]
        public int EventTypeID { get; set; }
        public EventType EventType { get; set; }
        [Key, ForeignKey("EventSetup")]
        [Required]
        [DisplayName("Set Up")]
        public int SetUpID { get; set; }
        public EventSetup EventSetup { get; set; }
        [Key, ForeignKey("HallDetail")]
        [DisplayName("Hall")]
        public int HallID { get; set; }
        public HallDetail HallDetail { get; set; }
        [DisplayName("Event Date")]
        public DateTime EventDate { get; set; }
        [DisplayName("Invite Date")]
        public DateTime InviteDate { get; set; }
        public string Remarks { get; set; }
        public ICollection<MemberDetails> MemberDetailsList { get; set; }
    }
}
