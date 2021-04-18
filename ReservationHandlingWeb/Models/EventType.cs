using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationHandlingWeb.Models
{
    public class EventType
    {
        public int ID { get; set; }
        [DisplayName("Event Type")]
        public string Type { get; set; }
        public string Description { get; set; }
    }
}
