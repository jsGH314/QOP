using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QOP.DAL;
using QOP.DAL.Repository.IRepository;
using QOP.Models;
using QOP.Utility;

namespace QOP.Areas.Staff.Controllers
{
    [Area("Staff")]
    [Authorize(Roles = SD.Role_Staff)]
    public class TimeSlotController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public TimeSlotController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: Staff/TimeSlot
        public async Task<IActionResult> Index()
        {
            //return View(await _context.TimeSlots.ToListAsync());

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<TimeSlot> objTimeSlotList = _unitOfWork.TimeSlot.GetAll().Where(u => u.ApplicationUserId == userId).ToList();
            return View(objTimeSlotList);
        }

        // GET: Staff/TimeSlot/Details/5
        public async Task<IActionResult> Details(int? slotId)
        {
            TimeSlot timeSlot = _unitOfWork.TimeSlot.Get(u => u.Id == slotId);
            return View(timeSlot);

            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var timeSlot = await _context.TimeSlots
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (timeSlot == null)
            //{
            //    return NotFound();
            //}

            //return View(timeSlot);
        }

        // GET: Staff/TimeSlot/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Staff/TimeSlot/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SlotDate,SlotTime,isAvailable,isTransportSlot")] TimeSlot obj)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

            if (ModelState.IsValid)
            {
                obj.ApplicationUser = appUser;
                obj.ApplicationUserId = userId;

                _unitOfWork.TimeSlot.Add(obj);
                //await _context.SaveChangesAsync();
                _unitOfWork.Save();
                TempData["success"] = "Time Slot Added Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET: Staff/TimeSlot/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var timeSlot = await _context.TimeSlots.FindAsync(id);
            TimeSlot? slotFromDb = _unitOfWork.TimeSlot.Get(u => u.Id == id);
            if (slotFromDb == null)
            {
                return NotFound();
            }
            return View(slotFromDb);
        }

        // POST: Staff/TimeSlot/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SlotDate,SlotTime,isAvailable,isTransportSlot")] TimeSlot obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.TimeSlot.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Time Slot Updated Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Staff/TimeSlot/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TimeSlot? slotfromDb = _unitOfWork.TimeSlot.Get(u => u.Id == id);

            if (slotfromDb == null)
            {
                return NotFound();
            }

            return View(slotfromDb);
        }

        // POST: Staff/TimeSlot/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            TimeSlot? obj = _unitOfWork.TimeSlot.Get(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.TimeSlot.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Time Slot Deleted Successfully";

            return RedirectToAction("Index");
        }

        //private bool TimeSlotExists(int id)
        //{
        //    return _context.TimeSlots.Any(e => e.Id == id);
        //}
    }
}
