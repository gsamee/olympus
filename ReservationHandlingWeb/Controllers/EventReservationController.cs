using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservationHandlingWeb.Data;
using ReservationHandlingWeb.Models;
using ReservationHandlingWeb.Services;
using ReservationHandlingWeb.ViewModels;

namespace ReservationHandlingWeb.Controllers
{
    public class EventReservationController : Controller
    {
        private readonly ReservationDBContext _context;
        private CommonService CS;

        public EventReservationController(ReservationDBContext context, CommonService commonService)
        {
            _context = context;
            CS = commonService;
        }

        EventRegistrationVM eventRegistrationVM = new EventRegistrationVM();
        // GET: EventReservation
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserMain.ToListAsync());
        }

        // GET: EventReservation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userMain = await _context.UserMain.Include(x => x.EventType).Include(y => y.EventSetup).Include(z => z.HallDetail)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userMain == null)
            {
                return NotFound();
            }

            return View(userMain);
        }

        private async Task FillDropdown(EventRegistrationVM VM)
        {
            VM.HallDetailList = await _context.HallDetail?.Select(s => new SelectListItem
            {
                Value = s.ID.ToString(),
                Text = s.Name
            }).ToListAsync();

            VM.EventTypeList = await _context.EventType?.Select(s => new SelectListItem
            {
                Value = s.ID.ToString(),
                Text = s.Type
            }).ToListAsync();

            VM.EventSetupList = await _context.EventSetup?.Select(s => new SelectListItem
            {
                Value = s.ID.ToString(),
                Text = s.SetUp
            }).ToListAsync();
        }

        // GET: EventReservation/Create
        public async Task<IActionResult> Create()
        {
            //eventRegistrationVM.HallDetailList = _context.HallDetail?.Select(s => new SelectListItem
            //{
            //    Value = s.ID.ToString(),
            //    Text = s.Name
            //});

            //eventRegistrationVM.EventTypeList = _context.EventType?.Select(s => new SelectListItem
            //{
            //    Value = s.ID.ToString(),
            //    Text = s.Type
            //});

            //eventRegistrationVM.EventSetupList = _context.EventSetup?.Select(s => new SelectListItem
            //{
            //    Value = s.ID.ToString(),
            //    Text = s.SetUp
            //});
            await FillDropdown(eventRegistrationVM);

            return View(eventRegistrationVM);
        }

        // POST: EventReservation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserMain")] EventRegistrationVM eventRegistrationVM)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventRegistrationVM.UserMain);
                await _context.SaveChangesAsync();
                int ID = eventRegistrationVM.UserMain.ID;
                CS.MailSend(eventRegistrationVM.UserMain.Email, ID, "CSV"); // pass the csv file

                return RedirectToAction(nameof(Index));
            }
            await FillDropdown(eventRegistrationVM);
            return View(eventRegistrationVM);
        }

        // GET: EventReservation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userMain = await _context.UserMain.FindAsync(id);
            if (userMain == null)
            {
                return NotFound();
            }
            eventRegistrationVM.UserMain = userMain;
            await FillDropdown(eventRegistrationVM);
            return View(eventRegistrationVM);
        }

        // POST: EventReservation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserMain")] EventRegistrationVM eventRegistrationVM)
        {
            if (id != eventRegistrationVM.UserMain.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventRegistrationVM.UserMain);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserMainExists(eventRegistrationVM.UserMain.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(eventRegistrationVM);
        }

        // GET: EventReservation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userMain = await _context.UserMain
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userMain == null)
            {
                return NotFound();
            }

            return View(userMain);
        }

        // POST: EventReservation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userMain = await _context.UserMain.FindAsync(id);
            _context.UserMain.Remove(userMain);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserMainExists(int id)
        {
            return _context.UserMain.Any(e => e.ID == id);
        }
    }
}
