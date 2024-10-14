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
    public class TimeSlotRepository : Repository<TimeSlot>, ITimeSlotRepository
    {
        private readonly ApplicationDbContext _db;
        public TimeSlotRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(TimeSlot obj)
        {
            var objFromDb = _db.TimeSlots.FirstOrDefault(s => s.Id == obj.Id);
            if (objFromDb != null)
            {
                //objFromDb.ClientId = obj.ClientId;
                //objFromDb.EmployeeId = obj.EmployeeId;
                objFromDb.SlotDate = obj.SlotDate;
                objFromDb.SlotTime = obj.SlotTime;
                objFromDb.isAvailable = obj.isAvailable;
                objFromDb.isTransportSlot = obj.isTransportSlot;
            }
        }
    }
}
