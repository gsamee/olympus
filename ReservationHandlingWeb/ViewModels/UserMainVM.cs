using ReservationHandlingWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationHandlingWeb.ViewModels
{
    public class UserMainVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        [DisplayName("Event Type")]
        public EventType EventType { get; set; }
        [DisplayName("Set Up")]
        public int SetUpID { get; set; }

        [DisplayName("Hall")]
        public int HallID { get; set; }
        [DisplayName("Event Date")]
        public DateTime EventDate { get; set; }
        [DisplayName("Invite Date")]
        public DateTime InviteDate { get; set; }
        public string Remarks { get; set; }
    }
}
