using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Infrastructure.Data;
using Library.Infrastructure.Repositories.ReservationRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.Repositories.Reservations
{
    public class ReservationsRepositoryTests
    {
        private ReservationRepository GetRepository(LibraryDbContext context)
        {
            return new ReservationRepository(context);
        }

        private DbContextOptions<LibraryDbContext> GetInMemoryOptions(string dbName)
        {
            return new DbContextOptionsBuilder<LibraryDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
        }

        [Fact]
        public async Task AddAsync_StoresCorrectBookType()
        {
            var options = GetInMemoryOptions("AddAsync_StoresCorrectBookType_Test");

            using (var context = new LibraryDbContext(options))
            {
                var repository = GetRepository(context);

                var newReservation = new Reservation
                {
                    ReservationId = 1,
                    BookId = 1,
                    Type = BookType.AudioBook,
                    DaysReserved = 5,
                    QuickPickup = true,
                    TotalCost = 20.00m
                };

                await repository.AddAsync(newReservation);
            }

            using (var context = new LibraryDbContext(options))
            {
                var reservation = await context.Reservations.FindAsync(1);
                Assert.NotNull(reservation);
                Assert.Equal(BookType.AudioBook, reservation.Type); 
                Assert.Equal(20.00m, reservation.TotalCost);
            }
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllReservations()
        {
            var options = GetInMemoryOptions("GetAllReservationsAsync_Test");

            using (var context = new LibraryDbContext(options))
            {
                context.Reservations.AddRange(
                    new Reservation { ReservationId = 1, BookId = 1, Type = BookType.Book, DaysReserved = 7, QuickPickup = false, TotalCost = 15.00m },
                    new Reservation { ReservationId = 2, BookId = 2, Type = BookType.AudioBook, DaysReserved = 3, QuickPickup = true, TotalCost = 10.00m }
                );
                context.SaveChanges();
            }

            using (var context = new LibraryDbContext(options))
            {
                var repository = GetRepository(context);
                var result = await repository.GetAllAsync();

                Assert.Equal(2, result.Count());
            }
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectReservation()
        {
            var options = GetInMemoryOptions("GetReservationByIdAsync_Test");

            using (var context = new LibraryDbContext(options))
            {
                context.Reservations.Add(new Reservation { ReservationId = 1, BookId = 1, Type = BookType.Book, DaysReserved = 7, QuickPickup = false, TotalCost = 15.00m });
                context.SaveChanges();
            }

            using (var context = new LibraryDbContext(options))
            {
                var repository = GetRepository(context);
                var result = await repository.GetByIdAsync(1);

                Assert.NotNull(result);
                Assert.Equal(1, result.ReservationId);
                Assert.Equal(BookType.Book, result.Type);
            }
        }

        [Fact]
        public async Task UpdateAsync_UpdatesExistingReservation()
        {
            var options = GetInMemoryOptions("UpdateReservationAsync_Test");

            using (var context = new LibraryDbContext(options))
            {
                context.Reservations.Add(new Reservation { ReservationId = 1, BookId = 1, Type = BookType.Book, DaysReserved = 7, QuickPickup = false, TotalCost = 15.00m });
                context.SaveChanges();
            }

            using (var context = new LibraryDbContext(options))
            {
                var repository = GetRepository(context);
                var reservation = await context.Reservations.FindAsync(1);
                reservation.Type = BookType.AudioBook;
                reservation.QuickPickup = true;
                reservation.TotalCost = 25.00m;
                await repository.UpdateAsync(reservation);
            }

            using (var context = new LibraryDbContext(options))
            {
                var updatedReservation = await context.Reservations.FindAsync(1);
                Assert.Equal(BookType.AudioBook, updatedReservation.Type);
                Assert.True(updatedReservation.QuickPickup);
                Assert.Equal(25.00m, updatedReservation.TotalCost);
            }
        }

        [Fact]
        public async Task DeleteAsync_RemovesReservation()
        {
            var options = GetInMemoryOptions("DeleteReservationAsync_Test");

            using (var context = new LibraryDbContext(options))
            {
                context.Reservations.Add(new Reservation { ReservationId = 1, BookId = 1, Type = BookType.Book, DaysReserved = 7, QuickPickup = false, TotalCost = 15.00m });
                context.SaveChanges();
            }

            using (var context = new LibraryDbContext(options))
            {
                var repository = GetRepository(context);
                var reservation = await repository.GetByIdAsync(1);
                await repository.DeleteAsync(reservation);
            }

            using (var context = new LibraryDbContext(options))
            {
                var reservation = await context.Reservations.FindAsync(1);
                Assert.Null(reservation);
            }
        }
    }
}
