using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QOP.Models.ViewModels
{
    public class JournalVM
    {
        public Journal Journal { get; set; }
        public IEnumerable<Journal> JournalList { get; set; }


    }
}
