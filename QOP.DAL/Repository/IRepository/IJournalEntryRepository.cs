using QOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QOP.DAL.Repository.IRepository
{
    public interface IJournalEntryRepository : IRepository<JournalEntry>
    {
        void Update(JournalEntry obj);
    }
}
