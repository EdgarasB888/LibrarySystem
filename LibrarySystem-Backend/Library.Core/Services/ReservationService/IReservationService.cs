using Library.Core.Services.Generic;
using Library.Domain.Entities;
using Library.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Services.ReservationService
{
    public interface IReservationService : IGenericService<Reservation>
    {
        Task<Reservation> CreateReservationAsync(int bookId, BookType bookType, int days, bool quickPickup);
    }
}
