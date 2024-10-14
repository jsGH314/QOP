using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QOP.Models
{
    public class TestModel
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime SlotDate { get; set; }
        [DataType(DataType.Time)]
        public DateTime SlotTime { get; set; }
        public bool isAvailable { get; set; }
        public bool isTransportSlot { get; set; }
    }
}
