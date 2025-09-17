using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QOP.Models.ViewModels
{
    public class JournalEntryVM
    {
        public JournalEntry JournalEntry { get; set; }
        public IEnumerable<JournalEntry> JournalEntryList { get; set; }

        public void OnGet()
        {
            JournalEntry = new JournalEntry
            {
                EntryDate = DateTime.Now
            };
        }


    }
}
