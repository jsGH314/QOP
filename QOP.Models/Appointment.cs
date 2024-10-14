using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QOP.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        public string? ClientId { get; set; }
        //[ForeignKey("Staff")]
        //public int StaffId { get; set; }

        public string? ClientName { get; set; }
        public string StaffName { get; set; }
        public bool isTransportAppt { get; set; }
        [DataType(DataType.Date)]
        public DateTime ApptDate { get; set; }
        [DataType(DataType.Time)]
        public DateTime ApptTime { get; set; }
        public string? status { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        public Appointment()
        {
            //EntryDate = DateTime.Now;
            isTransportAppt = false;
            ApptDate = DateTime.Now;
            ApptTime = DateTime.Now;
            ApplicationUser = new ApplicationUser();
            ApplicationUserId = ApplicationUser.Id;
            StaffName = "Staff";
            ClientName = "Client";
            //;
        }
    }
}
