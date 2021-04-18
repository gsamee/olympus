using Microsoft.AspNetCore.Mvc.Rendering;
using ReservationHandlingWeb.Models;
using System.Collections.Generic;

namespace ReservationHandlingWeb.ViewModels
{
    public class EventRegistrationVM
    {
        public UserMain UserMain { get; set; }
        public IEnumerable<SelectListItem> HallDetailList { get; set; }
        public IEnumerable<SelectListItem> EventTypeList { get; set; }
        public IEnumerable<SelectListItem> EventSetupList { get; set; }
    }
}
