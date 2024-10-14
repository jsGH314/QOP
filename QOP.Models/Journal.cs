using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QOP.Models
{
    //"Shopping Cart"
    public class Journal
    {
        [Key]
        public int Id { get; set; }
        //"Product"
        /// <summary>
        /// public int JournalEntryId { get; set; }
        /// </summary>
        //[ForeignKey("JournalEntryId")]
        //[ValidateNever]
        //public JournalEntry JournalEntry { get; set; }

        //public string ApplicationUserId { get; set; }
        //[ForeignKey("ApplicationUserId")]
        //[ValidateNever]
        //public ApplicationUser ApplicationUser { get; set; }
        //public int ClientId { get; set; }


        //[ForeignKey("Appointment")]
        //public int? AppointmentId { get; set; }
        //public string? JournalEntry { get; set; }
        //public DateTime EntryDate { get; set; }
    }
}
