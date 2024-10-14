using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QOP.Models.ViewModels
{
    public class ClientHomeVM
    {
        public IEnumerable<JournalEntry> EntryList { get; set; }
        public IEnumerable<Appointment> AppointmentList { get; set; }
        public IEnumerable<Appointment> TimeSlotList { get; set; }
    }
}
