using Library.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReservationId { get; set; }
        public int BookId { get; set; }
        public BookType Type { get; set; }
        public int DaysReserved { get; set; }
        public bool QuickPickup { get; set; }
        public decimal TotalCost { get; set; }
    }
}
