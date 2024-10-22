using Library.Core.Services.BookService;
using Library.Core.Services.Generic;
using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Infrastructure.Repositories.BookRepository;
using Library.Infrastructure.Repositories.Generic;
using Library.Infrastructure.Repositories.ReservationRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Services.ReservationService
{
    public class ReservationService : GenericService<Reservation>, IReservationService
    {
        public ReservationService(IReservationRepository reservationRepository) : base(reservationRepository)
        {

        }

        public async Task<Reservation> CreateReservationAsync(int bookId, BookType bookType, int days, bool quickPickup)
        {
            decimal costPerDay = bookType == BookType.Book ? 2 : 3;
            decimal discount = days > 10 ? 0.20m : (days > 3 ? 0.10m : 0);
            decimal totalCost = days * costPerDay;
            totalCost -= totalCost * discount;
            totalCost += 3;

            if (quickPickup)
            {
                totalCost += 5;
            }

            var reservation = new Reservation
            {
                BookId = bookId,
                Type = bookType,
                DaysReserved = days,
                QuickPickup = quickPickup,
                TotalCost = totalCost
            };

            await _repository.AddAsync(reservation);
            return reservation;
        }
    }
}
