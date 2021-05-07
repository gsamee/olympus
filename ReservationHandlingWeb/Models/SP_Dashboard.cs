using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationHandlingWeb.Models
{
    public class SP_Dashboard
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int NoOfMembers { get; set; }
        public string SeatNo { get; set; }
        public int AttStatus { get; set; }
    }
}
