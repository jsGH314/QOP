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
using QOP.DAL.Migrations;
using QOP.DAL.Repository.IRepository;
using QOP.Models;
using QOP.Models.ViewModels;
using QOP.Utility;

namespace QOP.Areas.Staff.Controllers
{
    [Area("Staff")]
    //[Authorize(Roles = SD.Role_Admin)]
    //[Authorize(Roles = SD.Role_Staff)]
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Admin/Appointment
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<Appointment> objAppointmentList = _unitOfWork.Appointment.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Appointment").ToList();
            //if(objAppointmentList == null)
            //{
            //    return View();
            //}
            //else
            //{
            //    return View(objAppointmentList);
            //}
            return View(objAppointmentList);

        }

        // GET: Admin/Appointment/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? appointmentId)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var appointment = await _context.Appointments
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (appointment == null)
            //{
            //    return NotFound();
            //}

            //return View(appointment);
            Appointment appointment = _unitOfWork.Appointment.Get(u => u.Id == appointmentId, includeProperties: "Appointment");
            return View(appointment);
        }

        // GET: Admin/Appointment/Create
        public IActionResult Create()
        {
            var users = _unitOfWork.ApplicationUser.GetAll().Select(u => new { u.Id, u.Name }).ToList();
            ViewBag.ClientList = new SelectList(users, "Id", "Name");
            return View();   
        }

        // POST: Admin/Appointment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Appointment obj)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
            var appUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);
            string staffName = appUser?.Name ?? string.Empty;

            // Assign ClientId and ClientName if a client is selected in the dropdown
            if (!string.IsNullOrEmpty(obj.ClientId))
            {
                var clientUser = _unitOfWork.ApplicationUser.Get(u => u.Id == obj.ClientId);
                if (clientUser != null && !string.IsNullOrEmpty(clientUser.Name))
                {
                    obj.ClientName = clientUser.Name;
                }
                else
                {
                    obj.ClientName = string.Empty;
                }
            }
            else
            {
                obj.ClientId = string.Empty;
                obj.ClientName = string.Empty;
            }

            if (ModelState.IsValid)
            {
                obj.ApplicationUser = appUser!;
                obj.ApplicationUserId = userId;
                obj.StaffName = staffName;
                _unitOfWork.Appointment.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Appointment Added Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET: Admin/Appointment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var appointment = await _context.Appointments.FindAsync(id);
            Appointment? apptFromDb = _unitOfWork.Appointment.Get(u => u.Id == id);
            if (apptFromDb == null)
            {
                return NotFound();
            }
            return View(apptFromDb);
        }

        // POST: Admin/Appointment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Appointment obj)//int id, [Bind("Id,ClientId,StaffId,isTransportAppt,ApptDate,ApptTime,status")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Appointment.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Appointment Updated Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> ClaimSlot(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var appointment = await _context.Appointments.FindAsync(id);
            Appointment? apptFromDb = _unitOfWork.Appointment.Get(u => u.Id == id);
            if (apptFromDb == null)
            {
                return NotFound();
            }
            return View(apptFromDb);
        }

        // POST: Admin/Appointment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClaimSlot(Appointment obj)//int id, [Bind("Id,ClientId,StaffId,isTransportAppt,ApptDate,ApptTime,status")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Appointment.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Appointment Updated Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Admin/Appointment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var appointment = await _context.Appointments
            //.FirstOrDefaultAsync(m => m.Id == id);

            Appointment? apptfromDb = _unitOfWork.Appointment.Get(u => u.Id == id);
            if (apptfromDb == null)
            {
                return NotFound();
            }

            return View(apptfromDb);
        }

        // POST: Admin/Appointment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            Appointment? obj = _unitOfWork.Appointment.Get(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Appointment.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Appointment Deleted Successfully";

            return RedirectToAction("Index");
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.Id == id);
        }
    }
}
