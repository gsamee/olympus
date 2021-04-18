using System.ComponentModel;

namespace ReservationHandlingWeb.Models
{
    public class EventSetup
    {
        public int ID { get; set; }
        [DisplayName("Set Up")]
        public string SetUp { get; set; }
        public string Description { get; set; }
    }
}
