using System.ComponentModel;

namespace ReservationHandlingWeb.Models
{
    public class HallDetail
    {
        public int ID { get; set; }
        [DisplayName("Hall")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
