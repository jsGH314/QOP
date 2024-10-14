using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QOP.Models
{
    public class StaffMember
    {
        [Key]
        public int Id { get; set; }
        //[ForeignKey("ApplicationUser")]
        //public int ApplicationUserId { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
