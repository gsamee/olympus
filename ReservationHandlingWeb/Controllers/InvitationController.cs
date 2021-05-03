using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReservationHandlingWeb.Data;
using ReservationHandlingWeb.Services;

namespace ReservationHandlingWeb.Controllers
{
    public class InvitationController : Controller
    {

        private readonly ReservationDBContext _context;
        private InvitationService CS;

        public InvitationController(ReservationDBContext context, InvitationService invitationService)
        {
            _context = context;
            CS = invitationService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Invite()
        {
            await CS.SendInvitation();

            return new JsonResult(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> Remind()
        {
            await CS.SendInvitation(true);

            return new JsonResult(new { success = true });
        }
    }
}
