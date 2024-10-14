using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QOP.Models
{
    public class TimeSlot
    {
        [Key]
        public int Id { get; set; }
        //[ForeignKey("Staff")]
        //public int StaffId { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime SlotDate { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        public DateTime SlotTime { get; set; }
        public bool isAvailable { get; set; }
        public bool isTransportSlot { get; set; }

        //public TimeSlot()
        //{
        //    ApplicationUser = new ApplicationUser();
        //    ApplicationUserId = ApplicationUser.Id;
        //}
    }
}
