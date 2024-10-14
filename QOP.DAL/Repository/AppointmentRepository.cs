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
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        private readonly ApplicationDbContext _db;
        public AppointmentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Appointment obj)
        {
            var objFromDb = _db.Appointments.FirstOrDefault(s => s.Id == obj.Id);
            if (objFromDb != null)
            {
                //objFromDb.ClientId = obj.ClientId;
                //objFromDb.EmployeeId = obj.EmployeeId;
                objFromDb.ApptDate = obj.ApptDate;
                objFromDb.ApptTime = obj.ApptTime;
                objFromDb.isTransportAppt = obj.isTransportAppt;
                objFromDb.status = obj.status;
            }
        }
    }
}
