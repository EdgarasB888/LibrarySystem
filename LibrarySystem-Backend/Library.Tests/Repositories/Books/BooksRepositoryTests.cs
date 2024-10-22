using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Library.Infrastructure.Repositories.BookRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.Repositories.Books
{
    public class BooksRepositoryTests
    {
        private BookRepository GetRepository(LibraryDbContext context)
        {
            return new BookRepository(context);
        }

        private DbContextOptions<LibraryDbContext> GetInMemoryOptions(string dbName)
        {
            return new DbContextOptionsBuilder<LibraryDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllBooks()
        {
            var options = GetInMemoryOptions("GetAllAsyncTest");

            using (var context = new LibraryDbContext(options))
            {
                context.Books.AddRange(
                    new Book { BookId = 1, Name = "Book One", Year = 2021, PictureUrl = "http://example.com/book1.jpg" },
                    new Book { BookId = 2, Name = "Book Two", Year = 2022, PictureUrl = "http://example.com/book2.jpg" }
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
        public async Task GetByIdAsync_ReturnsCorrectBook()
        {
            var options = GetInMemoryOptions("GetByIdAsyncTest");

            using (var context = new LibraryDbContext(options))
            {
                context.Books.Add(new Book { BookId = 1, Name = "Book One", Year = 2021, PictureUrl = "http://example.com/book1.jpg" });
                context.SaveChanges();
            }

            using (var context = new LibraryDbContext(options))
            {
                var repository = GetRepository(context);
                var result = await repository.GetByIdAsync(1);

                Assert.NotNull(result);
                Assert.Equal(1, result.BookId);
            }
        }

        [Fact]
        public async Task AddAsync_AddsNewBook()
        {
            var options = GetInMemoryOptions("AddAsyncTest");

            using (var context = new LibraryDbContext(options))
            {
                var repository = GetRepository(context);

                var newBook = new Book { BookId = 1, Name = "New Book", Year = 2023, PictureUrl = "http://example.com/newbook.jpg" };
                await repository.AddAsync(newBook);
            }

            using (var context = new LibraryDbContext(options))
            {
                var book = await context.Books.FindAsync(1);
                Assert.NotNull(book);
                Assert.Equal("New Book", book.Name);
            }
        }

        [Fact]
        public async Task UpdateAsync_UpdatesExistingBook()
        {
            var options = GetInMemoryOptions("UpdateAsyncTest");

            using (var context = new LibraryDbContext(options))
            {
                context.Books.Add(new Book { BookId = 1, Name = "Old Book", Year = 2021, PictureUrl = "http://example.com/oldbook.jpg" });
                context.SaveChanges();
            }

            using (var context = new LibraryDbContext(options))
            {
                var repository = GetRepository(context);
                var book = await context.Books.FindAsync(1);
                book.Name = "Updated Book";
                await repository.UpdateAsync(book);
            }

            using (var context = new LibraryDbContext(options))
            {
                var book = await context.Books.FindAsync(1);
                Assert.Equal("Updated Book", book.Name);
            }
        }

        [Fact]
        public async Task DeleteAsync_RemovesBook()
        {
            var options = GetInMemoryOptions("DeleteAsyncTest");

            using (var context = new LibraryDbContext(options))
            {
                context.Books.Add(new Book { BookId = 1, Name = "Book to Delete", Year = 2021, PictureUrl = "http://example.com/booktodelete.jpg" });
                context.SaveChanges();
            }

            using (var context = new LibraryDbContext(options))
            {
                var repository = GetRepository(context);
                var book = await repository.GetByIdAsync(1);
                await repository.DeleteAsync(book);
            }

            using (var context = new LibraryDbContext(options))
            {
                var book = await context.Books.FindAsync(1);
                Assert.Null(book);
            }
        }

        [Fact]
        public async Task SearchAsync_ReturnsMatchingBooks()
        {
            var options = GetInMemoryOptions("SearchAsyncTest");

            using (var context = new LibraryDbContext(options))
            {
                context.Books.AddRange(
                    new Book { BookId = 1, Name = "C# Programming", Year = 2021, PictureUrl = "http://example.com/csharp.jpg" },
                    new Book { BookId = 2, Name = "Java Programming", Year = 2020, PictureUrl = "http://example.com/java.jpg" }
                );
                context.SaveChanges();
            }

            using (var context = new LibraryDbContext(options))
            {
                var repository = GetRepository(context);
                var result = await repository.SearchAsync("C#");

                Assert.Single(result);
                Assert.Equal("C# Programming", result.First().Name);
            }
        }

        [Fact]
        public async Task SearchAsync_ReturnsEmpty_WhenNoMatchesFound()
        {
            var options = GetInMemoryOptions("SearchAsyncNoMatchTest");

            using (var context = new LibraryDbContext(options))
            {
                context.Books.AddRange(
                    new Book { BookId = 1, Name = "C# Programming", Year = 2021, PictureUrl = "http://example.com/csharp.jpg" },
                    new Book { BookId = 2, Name = "Java Programming", Year = 2020, PictureUrl = "http://example.com/java.jpg" }
                );
                context.SaveChanges();
            }

            using (var context = new LibraryDbContext(options))
            {
                var repository = GetRepository(context);
                var result = await repository.SearchAsync("Python");

                Assert.Empty(result);
            }
        }
    }
}
