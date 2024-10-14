using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QOP.DAL.Repository.IRepository;
using QOP.Models;
using System;
using System.Collections.Generic;
using QOP.Models.ViewModels;
using QOP.Utility;
using QOP.DAL;
using System.Security.Claims;

namespace QOP.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorize]
    //"Shopping Cart Controller"
    public class JournalController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public JournalVM JournalVM { get; set; }

        public JournalController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            //JournalVM = new()
            //{
            //    JournalList = _unitOfWork.Journal.GetAll(u => u.ApplicationUserId == userId)//,
            //    //includeProperties: "Product"),
            //    //OrderHeader = new()
            //};

            List<JournalEntry> journalEntries = _unitOfWork.JournalEntry.GetAll().ToList();

            //foreach (var journal in JournalVM.JournalList)
            //{
            //    journal.JournalEntry.Entry = journalEntries.Where(u => u.ProductId == cart.Product.Id).ToList();
            //    journal.JournalEntry.Entry = journalEntries.Where(u => u.Id == journal.JournalEntry.Id).ToList();
            //    JournalVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            //}

            return View();
        }




    }
}
