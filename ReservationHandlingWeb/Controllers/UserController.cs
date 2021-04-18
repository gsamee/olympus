using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReservationHandlingWeb.Data;
using ReservationHandlingWeb.Services;

namespace ReservationHandlingWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly ReservationDBContext _context;
        private UserService CS;

        public UserController(ReservationDBContext context, UserService userService)
        {
            _context = context;
            CS = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Upload()
        {
            var path = @"D:\MemberDetails.csv";
            await CS.UploadMemberCSV(path);

            return new JsonResult(new { success = true });
        }
    }
}
