using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationHandlingWeb.Models
{
    public class Attendance
    {
        public int ID { get; set; }
        public int MemberID { get; set; }
        public bool Status { get; set; }
        public DateTime InTime { get; set; }
    }
}
