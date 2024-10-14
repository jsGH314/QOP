using Microsoft.AspNetCore.Mvc;
using QOP.DAL.Repository.IRepository;
using QOP.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using QOP.Utility;
using QOP.Models.ViewModels;

namespace QOP.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        //[HttpPost]
        [Authorize]
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(User.IsInRole(SD.Role_Client))
            {
                IEnumerable<JournalEntry> entryList = _unitOfWork.JournalEntry.GetAll(u => u.ApplicationUserId == userId, includeProperties: "JournalEntry");
                IEnumerable<Appointment> appointmentList = _unitOfWork.Appointment.GetAll(u => u.ClientId == userId, includeProperties: "Appointment");
                ClientHomeVM homeVm = new()
                {
                    EntryList = entryList,
                    AppointmentList = appointmentList
                };
                return View(homeVm);
            }
            else if(User.IsInRole(SD.Role_Staff))
            {
                IEnumerable<Appointment> appointmentList = _unitOfWork.Appointment.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Appointment");
                ClientHomeVM homeVm = new()
                {
                    AppointmentList = appointmentList
                };
                return View(homeVm);
            }
            else if(User.IsInRole(SD.Role_Admin))
            {
                IEnumerable<Appointment> appointmentList = _unitOfWork.Appointment.GetAll(includeProperties: "Appointment");
                ClientHomeVM homeVm = new()
                {
                    AppointmentList = appointmentList
                };
                return View(homeVm);
            }
            return View();

        }

        [Authorize]
        public IActionResult Schedule()
        {
            List<Appointment> timeSlotList = _unitOfWork.Appointment.GetAll(u => u.ClientId == null).ToList();
            //ClientHomeVM homeVm = new()
            //{
            //    TimeSlotList = timeSlotList
            //};
            return View(timeSlotList);
            //return View();
        }

        
        public async Task<IActionResult> GetSlot(int? slotId)
        {
            if (slotId == null)
            {
                return NotFound();
            }

        //    //var journalEntry = await _unitOfWork.JournalEntries.FindAsync(id);
            Appointment? slotFromDb = _unitOfWork.Appointment.Get(u => u.Id == slotId);
            if (slotFromDb == null)
            {
                return NotFound();
            }
            return View(slotFromDb);
        }

        public IActionResult ClaimSlot(int? id)
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
        public IActionResult ClaimSlot([Bind("Id,StaffId,isTransportAppt,ApptDate,ApptTime,status")] int id)
        {
            Appointment obj = _unitOfWork.Appointment.Get(u => u.Id == id);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);
            string clientName = appUser.Name;

            if (ModelState.IsValid)
            {
                obj.ClientId = userId;
                obj.ClientName = clientName;
                _unitOfWork.Appointment.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Appointment Updated Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }







        //public IActionResult ClaimSlot(int? id)
        //{

        //    if(id == null || id == 0)
        //    {
        //       return NotFound();
        //    }

        //    var clientId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    Appointment? slotFromDb = _unitOfWork.Appointment.Get(u => u.Id == id);
        //    if (slotFromDb == null)
        //    {
        //        return NotFound();
        //    }

        //    if(ModelState.IsValid)
        //    {
        //        slotFromDb.ClientId = clientId;
        //        _unitOfWork.Appointment.Update(slotFromDb);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Appointment Scheduled Successfully";
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ClaimSlot(int? id)//, [Bind("Id,ClientId,JournalId,AppointmentId,Entry,EntryDate")] JournalEntry journalEntry)
        //{
        //    var clientId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    Appointment obj = _unitOfWork.Appointment.Get(u => u.Id == id);

        //    if (ModelState.IsValid)
        //    {
        //        obj.ClientId = clientId;
        //        _unitOfWork.Appointment.Update(obj);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Appointment Scheduled Successfully";
        //        return RedirectToAction("Index");

        //    }
        //    //else
        //    //{
        //    //    appointmentVM.AppointmentList = _unitOfWork.Appointment.GetAll();
        //    //    return View(appointmentVM);
        //    //}
        //    return View();
        //}

        [Authorize]
        public IActionResult Details(int entryId)
        {
            JournalEntry journalEntry = _unitOfWork.JournalEntry.Get(u => u.Id == entryId, includeProperties: "JournalEntry");
            return View(journalEntry);
            //Journal journal = new Journal()
            //{
            //    JournalEntry = _unitOfWork.JournalEntry.Get(u => u.Id == entryId),
            //    JournalEntryId = entryId
            //};

            //JournalEntry entry = _unitOfWork.JournalEntry.Get(u => u.Id == entryId);//, includeProperties: "Journal");
            //return View();
            //JournalEntry journalEntry = new JournalEntry();
            //return View(journalEntry);
        }

        //[HttpPost]
        //[Authorize]
        //public IActionResult Details(JournalEntry journalEntry)
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var appUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);
        //    journalEntry.ApplicationUserId = userId;


        //    _unitOfWork.JournalEntry.Add(journalEntry);
        //    _unitOfWork.Save();

        //    return RedirectToAction(nameof(Index));
        //}



        //[HttpPost]
        //[Authorize]
        //public IActionResult Details()
        //{
        //    //var claimsIdentity = (ClaimsIdentity)User.Identity;
        //    //var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
        //    //journal.ApplicationUserId = userId;

        //    //Journal journalFromDb = _unitOfWork.Journal.Get(u => u.ApplicationUserId == userId &&
        //    //u.JournalEntryId == journal.JournalEntryId);

        //    //if (journalFromDb != null)
        //    //{
        //    //    _unitOfWork.Journal.Update(journalFromDb);
        //    //    _unitOfWork.Save();
        //    //}
        //    //else
        //    //{
        //    //    //add cart record
        //    //    _unitOfWork.Journal.Add(journal);
        //    //    _unitOfWork.Save();
        //    //    HttpContext.Session.SetInt32(SD.SessionJournal,
        //    //    _unitOfWork.Journal.GetAll(u => u.ApplicationUserId == userId).Count());
        //    //}
        //    //TempData["success"] = "Journal updated successfully";

        //    return RedirectToAction(nameof(Index));
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
