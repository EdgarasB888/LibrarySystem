using Library.Core.Services.ReservationService;
using Library.Domain.Entities;
using Library.Infrastructure.Repositories.ReservationRepository;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api/reservations")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetAllReservations()
        {
            var books = await _reservationService.GetAllAsync();
            return Ok(books);
        }

        [HttpGet]
        [Route("{reservationId}")]
        public async Task<ActionResult<Reservation>> GetReservationById(int reservationId)
        {
            var book = await _reservationService.GetByIdAsync(reservationId);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Reservation>> CreateReservation([FromBody] Reservation reservation)
        {
            if (reservation == null)
            {
                return BadRequest("Reservation cannot be null.");
            }

            var createdReservation = await _reservationService.CreateReservationAsync(
                reservation.BookId,
                reservation.Type,
                reservation.DaysReserved,
                reservation.QuickPickup
            );

            return CreatedAtAction(nameof(CreateReservation), createdReservation);
        }

        [HttpPut]
        [Route("{reservationId}")]
        public async Task<ActionResult> UpdateReservation(int reservationId, [FromBody] Reservation reservation)
        {
            if (reservationId != reservation.ReservationId)
            {
                return BadRequest("Reservation ID mismatch.");
            }

            await _reservationService.UpdateAsync(reservation);
            return NoContent();
        }

        [HttpDelete]
        [Route("{reservationId}")]
        public async Task<ActionResult> DeleteReservation(int reservationId)
        {
            var book = await _reservationService.GetByIdAsync(reservationId);
            if (book == null)
            {
                return NotFound();
            }

            await _reservationService.DeleteAsync(book);
            return NoContent();
        }
    }
}
