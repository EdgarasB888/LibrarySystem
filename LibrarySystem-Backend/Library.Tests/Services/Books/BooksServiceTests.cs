using Library.Core.Services.BookService;
using Library.Domain.Entities;
using Library.Infrastructure.Repositories.BookRepository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.Services.Books
{
    public class BooksServiceTests
    {
        private readonly Mock<IBookRepository> _mockBookRepository;
        private readonly BookService _bookService;

        public BooksServiceTests()
        {
            _mockBookRepository = new Mock<IBookRepository>();
            _bookService = new BookService(_mockBookRepository.Object);
        }

        [Fact]
        public async Task SearchAsync_ReturnsBooksMatchingQuery()
        {
            var query = "C Sharp";
            var books = new List<Book>
            {
                new Book { BookId = 1, Name = "C Sharp in Depth", Year = 2020, PictureUrl = "url1" },
                new Book { BookId = 2, Name = "Java Basics", Year = 2019, PictureUrl = "url2" }
            };
            _mockBookRepository.Setup(repo => repo.SearchAsync(query)).ReturnsAsync(books.Where(b => b.Name.Contains("C")).ToList());

            var result = await _bookService.SearchAsync(query);

            Assert.Single(result);
            Assert.Equal("C Sharp in Depth", result.First().Name);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllBooks()
        {
            var books = new List<Book>
            {
                new Book { BookId = 1, Name = "C Sharp in Depth", Year = 2020, PictureUrl = "url1" },
                new Book { BookId = 2, Name = "Java Basics", Year = 2019, PictureUrl = "url2" }
            };
            _mockBookRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(books);

            var result = await _bookService.GetAllAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsBook()
        {
            var book = new Book { BookId = 1, Name = "C Sharp in Depth", Year = 2020, PictureUrl = "url1" };
            _mockBookRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(book);

            var result = await _bookService.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("C Sharp in Depth", result.Name);
        }

        [Fact]
        public async Task CreateAsync_AddsBook()
        {
            var book = new Book { BookId = 1, Name = "C Sharp in Depth", Year = 2020, PictureUrl = "url1" };
            _mockBookRepository.Setup(repo => repo.AddAsync(book)).Returns(Task.CompletedTask);

            var result = await _bookService.CreateAsync(book);

            Assert.Equal(book, result);
            _mockBookRepository.Verify(repo => repo.AddAsync(book), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesBook()
        {
            var book = new Book { BookId = 1, Name = "C Sharp in Depth", Year = 2020, PictureUrl = "url1" };
            _mockBookRepository.Setup(repo => repo.UpdateAsync(book)).Returns(Task.CompletedTask);

            await _bookService.UpdateAsync(book);

            _mockBookRepository.Verify(repo => repo.UpdateAsync(book), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_DeletesBook()
        {
            var book = new Book { BookId = 1, Name = "C Sharp in Depth", Year = 2020, PictureUrl = "url1" };
            _mockBookRepository.Setup(repo => repo.DeleteAsync(book)).Returns(Task.CompletedTask);

            await _bookService.DeleteAsync(book);

            _mockBookRepository.Verify(repo => repo.DeleteAsync(book), Times.Once);
        }
    }
}
