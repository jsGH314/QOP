using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QOP.DAL.Repository.IRepository;
using QOP.Models;
using QOP.DAL;

namespace QOP.DAL.Repository
{
    public class JournalEntryRepository : Repository<JournalEntry>, IJournalEntryRepository
    {
        private readonly ApplicationDbContext _db;
        public JournalEntryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(JournalEntry obj)
        {
            var objFromDb = _db.JournalEntries.FirstOrDefault(s => s.Id == obj.Id);
            if (objFromDb != null)
            {
                //objFromDb.ClientId = obj.ClientId;
                //objFromDb.JournalId = obj.JournalId;
                objFromDb.AppointmentId = obj.AppointmentId;
                objFromDb.Entry = obj.Entry;
                objFromDb.EntryDate = obj.EntryDate;
            }
            //_db.JournalEntries.Update(obj);


            /*var objFromDb = _db.JournalEntries.FirstOrDefault(s => s.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.ClientId = obj.ClientId;
                objFromDb.JournalId = obj.JournalId;
                objFromDb.AppointmentId = obj.AppointmentId;
                objFromDb.Entry = obj.Entry;
                objFromDb.EntryDate = obj.EntryDate;
            }
            */
        }
    }
}
