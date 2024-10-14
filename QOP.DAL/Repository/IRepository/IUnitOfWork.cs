using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QOP.DAL.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IJournalEntryRepository JournalEntry { get; }
        IApplicationUserRepository ApplicationUser { get; }
        //IJournalRepository Journal { get; }
        IAppointmentRepository Appointment { get; }
        ITimeSlotRepository TimeSlot { get; }
        IStaffRepository StaffMember { get; }
        void Save();
    }
}
