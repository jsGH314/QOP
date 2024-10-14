using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QOP.Models
{
    public class JournalEntry
    {
        [Key]
        public int Id { get; set; }
        //[ForeignKey("Client")]
        //public string ClientId { get; set; }
        //[ForeignKey("Client")]
        
        
        //public int? JournalId { get; set; }
        //[ForeignKey("Journal")]
        
        public int? AppointmentId { get; set; }
        [ForeignKey("Appointment")]
        public string? Entry { get; set; }
        public DateTime EntryDate { get; set; } = DateTime.Now;
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        public JournalEntry()
        {
            EntryDate = DateTime.Now;
            ApplicationUser = new ApplicationUser();
            ApplicationUserId = ApplicationUser.Id;
            //;
        }
    }
}
