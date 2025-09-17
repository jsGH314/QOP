using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using QOP.DAL;
//using QOP.DAL.Migrations;
using QOP.DAL.Repository.IRepository;
using QOP.Models;
using QOP.Models.ViewModels;

namespace QOP.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorize]
    public class JournalEntryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public JournalEntryVM JournalEntryVM { get; set; }
        //public JournalVM JournalVM { get; set; }
        //private readonly ApplicationDbContext _context;
        //private readonly UserManager<IdentityUser> _userManager;

        public JournalEntryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_context = context;
            //_userManager = usermanager;
            //var userId = _userManager.GetUserId(User);
            //var userId = user.Id;
        }

        // GET: Client/JournalEntry
        [Authorize]
        public async Task<IActionResult> Index()
        {
            //var user = await _userManager.GetUserIdAsync();
            //string userId = user.Id;
            //string userId = user.Id;
            //var userId = user.Id;
            //var items = _context.JournalEntries.Where(item => item.ClientId == userId).ToList();
            //return View(items.ToList());

            //var userId = _userManager.GetUserId(User);
            //var CurrentUser = await _userManager.GetUserAsync(User);
            // if(CurrentUser != null)
            // {
            //   var items = _context.JournalEntries.Where(item => item.ClientId == CurrentUser.Id).ToList();
            //   return View(items.ToList());
            //}
            //return View("NotFound" );
            //return View(await _context.JournalEntries.ToListAsync());
            //var userId = _userManager.GetUserId();// FindFirstValue(ClaimTypes.U); // will give the user's userName
            //var aprojectContext = _context.Posts.Include(p => p.Category).Include(p => p.User).Where(p => p.User.UserName == userName);
            //return View(await aprojectContext.ToListAsync());
            //var identityUser = User.Identity;
            //var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //JournalEntryVM = new JournalEntryVM()
            //{
            //JournalEntryList = _unitOfWork.JournalEntry.GetAll(u => u.ApplicationUserId == userId, includeProperties:"JournalEntry").ToList()
            //};

            //return View(JournalEntryVM);
            //var claimsIdentity = (ClaimsIdentity)User.Identity;
            //var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            //JournalEntryVM = new JournalEntryVM()
            //{
            //    JournalEntryList = _unitOfWork.Journal.GetAll(u => u.ApplicationUserId == userId).ToList();
            //};
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //IEnumerable<JournalEntry> entryList = _unitOfWork.JournalEntry.GetAll(u => u.ApplicationUserId == userId, includeProperties: "JournalEntry");

            List<JournalEntry> objEntryList = _unitOfWork.JournalEntry.GetAll(u => u.ApplicationUserId == userId, includeProperties: "JournalEntry").ToList();

            return View(objEntryList);
        }

        [Authorize]
        public IActionResult Details(int entryId)
        {
            JournalEntry journalEntry = _unitOfWork.JournalEntry.Get(u => u.Id == entryId, includeProperties: "JournalEntry");
            return View(journalEntry);
        }

        public IActionResult Upsert(int? id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            JournalEntryVM journalEntryVM = new()
            {

                JournalEntryList = _unitOfWork.JournalEntry.GetAll(u => u.ApplicationUserId == userId),
                JournalEntry = new JournalEntry()
            };
            if (id == null || id == 0)
            {
                //create
                return View(journalEntryVM);
            }
            else
            {
                //update
                journalEntryVM.JournalEntry = _unitOfWork.JournalEntry.Get(u => u.Id == id);
                return View(journalEntryVM);
            }

        }

        // public async Task<IActionResult> MyJournal()
        // {
        //   var user = await _userManager.GetUserAsync(User);
        //   var userId = user.Id;
        //    var items = _context.JournalEntries.Where(item => item.ClientId == userId).ToList();
        //    return View(items.ToList());
        //}

        // GET: Client/JournalEntry/Details/5
        /*public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journalEntry = await _context.JournalEntries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (journalEntry == null)
            {
                return NotFound();
            }

            return View(journalEntry);
        }
        */

        // GET: Client/JournalEntry/Create
        public IActionResult Create()
        {
            var entry = new JournalEntry
            {
                EntryDate = DateTime.Now
            };
            return View(entry);
            //JournalEntryVM journalEntryVM = new()
            //{
            //    JournalEntry = new JournalEntry()
            //};
            //return View(journalEntryVM);
        }

        // POST: Client/JournalEntry/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,ApplicationUser,ApplicationUserId,Entry,EntryDate")] JournalEntry obj)
        {
            //var userId = _userManager.GetUserId(User);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //obj.ApplicationUserId = userId;
            //obj.ApplicationUser = _unitOfWork.ApplicationUser.Get();
            //obj.ApplicationUser = A
            var appUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);
            //obj.ApplicationUser = appUser;
            //_unitOfWork.JournalEntry.Add(obj);
            if (ModelState.IsValid)
            {
                obj.ApplicationUser = appUser;
                obj.ApplicationUserId = userId;
                //var userId = _userManager.GetUserId(User);
                //var userId = user.Id;
                //journalEntry.ClientId = userId;
                obj.EntryDate = DateTime.Now;
                _unitOfWork.JournalEntry.Add(obj);
                //await _unitOfWork.SaveChangesAsync();
                _unitOfWork.Save();
                TempData["success"] = "Journal Entry Added Successfully";   
                return RedirectToAction("Index");

                //return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }

        // GET: Client/JournalEntry/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var journalEntry = await _unitOfWork.JournalEntries.FindAsync(id);
            JournalEntry? entryFromDb = _unitOfWork.JournalEntry.Get(u => u.Id == id);
            if (entryFromDb == null)
            {
                return NotFound();
            }
            return View(entryFromDb);
        }

        // POST: Client/JournalEntry/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(JournalEntry obj)//, [Bind("Id,ClientId,JournalId,AppointmentId,Entry,EntryDate")] JournalEntry journalEntry)
        {
            //if (id != journalEntry.Id)
            //{
                //return NotFound();
            //}

            if (ModelState.IsValid)
            {
                _unitOfWork.JournalEntry.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Journal Entry Updated Successfully";
                return RedirectToAction("Index");

            }
            return View();
        }

        // GET: Client/JournalEntry/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            JournalEntry? entryFromDb = _unitOfWork.JournalEntry.Get(u => u.Id == id);

            //var journalEntry = await _context.JournalEntries
                //.FirstOrDefaultAsync(m => m.Id == id);
            if (entryFromDb == null)
            {
                return NotFound();
            }

            return View(entryFromDb);
        }

        // POST: Client/JournalEntry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            JournalEntry? obj = _unitOfWork.JournalEntry.Get(u => u.Id == id);

            //var journalEntry = await _context.JournalEntries.FindAsync(id);
            if (obj == null)
            {
                return NotFound();
                //_unitOfWork.JournalEntry.Remove(journalEntry);
            }
            _unitOfWork.JournalEntry.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Journal Entry Deleted Successfully";

            //await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        /*private bool JournalEntryExists(int id)
        {
            return _unitOfWork.JournalEntry.Any(e => e.Id == id);
        }
        */
    }
}
