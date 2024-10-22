using Library.API.Controllers;
using Library.Core.Services.ReservationService;
using Library.Domain.Entities;
using Library.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.Controllers.Reservations
{
    public class ReservationsControllerTests
    {
        private readonly Mock<IReservationService> _mockReservationService;
        private readonly ReservationsController _controller;

        public ReservationsControllerTests()
        {
            _mockReservationService = new Mock<IReservationService>();
            _controller = new ReservationsController(_mockReservationService.Object);
        }

        [Fact]
        public async Task GetAllReservations_ReturnsOkResult_WithListOfReservations()
        {
            var reservations = new List<Reservation>
            {
                new Reservation { ReservationId = 1, BookId = 1, Type = BookType.Book, DaysReserved = 7, QuickPickup = true },
                new Reservation { ReservationId = 2, BookId = 2, Type = BookType.AudioBook, DaysReserved = 14, QuickPickup = false }
            };

            _mockReservationService.Setup(service => service.GetAllAsync()).ReturnsAsync(reservations);

            var result = await _controller.GetAllReservations();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<Reservation>>(okResult.Value);
            Assert.Equal(reservations.Count, returnValue.Count());
        }

        [Fact]
        public async Task GetReservationById_ReturnsOkResult_WithReservation()
        {
            var reservationId = 1;
            var reservation = new Reservation { ReservationId = reservationId, BookId = 1, Type = BookType.Book, DaysReserved = 7, QuickPickup = true };

            _mockReservationService.Setup(service => service.GetByIdAsync(reservationId)).ReturnsAsync(reservation);

            var result = await _controller.GetReservationById(reservationId);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Reservation>(okResult.Value);
            Assert.Equal(reservationId, returnValue.ReservationId);
        }

        [Fact]
        public async Task GetReservationById_ReturnsNotFound_WhenReservationDoesNotExist()
        {
            var reservationId = 1;

            _mockReservationService.Setup(service => service.GetByIdAsync(reservationId)).ReturnsAsync((Reservation)null);

            var result = await _controller.GetReservationById(reservationId);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateReservation_ReturnsCreatedAtActionResult()
        {
            var reservation = new Reservation { BookId = 1, Type = BookType.Book, DaysReserved = 7, QuickPickup = true };
            var createdReservation = new Reservation { ReservationId = 1, BookId = 1, Type = BookType.AudioBook, DaysReserved = 7, QuickPickup = true };

            _mockReservationService.Setup(service => service.CreateReservationAsync(reservation.BookId, reservation.Type, reservation.DaysReserved, reservation.QuickPickup))
                .ReturnsAsync(createdReservation);

            var result = await _controller.CreateReservation(reservation);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<Reservation>(createdResult.Value);
            Assert.Equal(createdReservation.ReservationId, returnValue.ReservationId);
        }

        [Fact]
        public async Task UpdateReservation_ReturnsNoContent()
        {
            var reservationId = 1;
            var reservation = new Reservation { ReservationId = reservationId, BookId = 1, Type = BookType.Book, DaysReserved = 7, QuickPickup = true };

            _mockReservationService.Setup(service => service.UpdateAsync(reservation)).Returns(Task.CompletedTask);

            var result = await _controller.UpdateReservation(reservationId, reservation);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateReservation_ReturnsBadRequest_WhenIdMismatch()
        {
            var reservationId = 1;
            var reservation = new Reservation { ReservationId = 2, BookId = 1, Type = BookType.AudioBook, DaysReserved = 7, QuickPickup = true };

            var result = await _controller.UpdateReservation(reservationId, reservation);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteReservation_ReturnsNoContent_WhenReservationIsDeleted()
        {
            var reservationId = 1;
            var reservation = new Reservation { ReservationId = reservationId, BookId = 1, Type = BookType.AudioBook, DaysReserved = 7, QuickPickup = true };

            _mockReservationService.Setup(service => service.GetByIdAsync(reservationId)).ReturnsAsync(reservation);
            _mockReservationService.Setup(service => service.DeleteAsync(reservation)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteReservation(reservationId);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteReservation_ReturnsNotFound_WhenReservationDoesNotExist()
        {
            var reservationId = 1;

            _mockReservationService.Setup(service => service.GetByIdAsync(reservationId)).ReturnsAsync((Reservation)null);

            var result = await _controller.DeleteReservation(reservationId);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
