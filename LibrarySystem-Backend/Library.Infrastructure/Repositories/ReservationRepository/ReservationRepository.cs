using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Library.Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories.ReservationRepository
{
    public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository 
    {
        private readonly LibraryDbContext _context;

        public ReservationRepository(LibraryDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
