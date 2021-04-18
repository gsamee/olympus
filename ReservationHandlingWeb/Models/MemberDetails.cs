using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationHandlingWeb.Models
{
    public class MemberDetails
    {
        public int ID { get; set; }
        [Key, ForeignKey("UserMain")]
        public int UserID { get; set; }
        public string Name { get; set; }
        public int NoOfMembers { get; set; }
        public string SeatNo { get; set; }
        public bool Meal1 { get; set; }
        public bool Meal2 { get; set; }
        public bool Meal3 { get; set; }
        public bool IsVeg { get; set; }
        public string Email { get; set; }
        public bool IsInvitationSent { get; set; }
        public bool IsReminderSent { get; set; }

        public UserMain UserMain { get; set; }
    }
}
