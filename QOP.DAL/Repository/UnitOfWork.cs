using Microsoft.AspNetCore.Identity;
using QOP.DAL.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QOP.DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        //private readonly UserManager<ApplicationUser> _userManager;
        public IJournalEntryRepository JournalEntry { get; private set; }

        public IAppointmentRepository Appointment { get; private set; }
        public ITimeSlotRepository TimeSlot { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IStaffRepository StaffMember { get; private set; }
        //public IJournalRepository Journal { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            ApplicationUser = new ApplicationUserRepository(_db);
            //Journal = new JournalRepository(_db);
            JournalEntry = new JournalEntryRepository(_db);
            Appointment = new AppointmentRepository(_db);
            TimeSlot = new TimeSlotRepository(_db);
            StaffMember = new StaffRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
