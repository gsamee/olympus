using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationHandlingWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationHandlingWeb.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ReservationDBContext _context;
        public DashboardController(ReservationDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Dashboard(int id)
        {
            try
            {
                var x = await _context.SP_Dashboard.FromSqlRaw("GetDashboard {0}", id).ToListAsync();
                return new JsonResult(x);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
    }
}
