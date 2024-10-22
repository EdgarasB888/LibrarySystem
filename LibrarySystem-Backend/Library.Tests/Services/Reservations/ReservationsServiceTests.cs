using Library.Core.Services.ReservationService;
using Library.Domain.Enums;
using Library.Infrastructure.Repositories.ReservationRepository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.Services.Reservations
{
    public class ReservationsServiceTests
    {
        private readonly Mock<IReservationRepository> _mockReservationRepository;
        private readonly ReservationService _reservationService;

        public ReservationsServiceTests()
        {
            _mockReservationRepository = new Mock<IReservationRepository>();
            _reservationService = new ReservationService(_mockReservationRepository.Object);
        }
    }
}
