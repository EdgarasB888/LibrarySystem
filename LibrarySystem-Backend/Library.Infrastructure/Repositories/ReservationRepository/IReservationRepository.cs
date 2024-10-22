using Library.Domain.Entities;
using Library.Infrastructure.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories.ReservationRepository
{
    public interface IReservationRepository : IGenericRepository<Reservation> 
    {
        //Made for adding extra functionality in the future
    }
}
