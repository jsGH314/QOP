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
    public class StaffRepository : Repository<StaffMember>, IStaffRepository
    {
        private readonly ApplicationDbContext _db;
        public StaffRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(StaffMember obj)
        {
            var objFromDb = _db.StaffMembers.FirstOrDefault(s => s.Id == obj.Id);
            if (objFromDb != null)
            {
                //objFromDb.ClientId = obj.ClientId;
                //objFromDb.EmployeeId = obj.EmployeeId;
                objFromDb.Name = obj.Name;
                //objFromDb.ApptTime = obj.ApptTime;
                //objFromDb.isTransportAppt = obj.isTransportAppt;
                //objFromDb.status = obj.status;
            }
        }
    }
}
