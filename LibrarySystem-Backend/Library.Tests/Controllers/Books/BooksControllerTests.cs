using Library.API.Controllers;
using Library.Core.Services.BookService;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.Controllers.Books
{
    public class BooksControllerTests
    {
        private readonly Mock<IBookService> _mockBookService;
        private readonly BooksController _controller;

        public BooksControllerTests()
        {
            _mockBookService = new Mock<IBookService>();
            _controller = new BooksController(_mockBookService.Object);
        }

        [Fact]
        public async Task GetAllBooks_ReturnsOkResult_WithListOfBooks()
        {
            var books = new List<Book>
            {
                new Book { BookId = 1, Name = "Book One", Year = 2021, PictureUrl = "http://example.com/book1.jpg" },
                new Book { BookId = 2, Name = "Book Two", Year = 2022, PictureUrl = "http://example.com/book2.jpg" }
            };

            _mockBookService.Setup(service => service.GetAllAsync()).ReturnsAsync(books);

            var result = await _controller.GetAllBooks();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<Book>>(okResult.Value);
            Assert.Equal(books.Count, returnValue.Count());
        }

        [Fact]
        public async Task GetBookById_ReturnsOkResult_WithBook()
        {
            var bookId = 1;
            var book = new Book { BookId = bookId, Name = "Book One", Year = 2021, PictureUrl = "http://example.com/book1.jpg" };

            _mockBookService.Setup(service => service.GetByIdAsync(bookId)).ReturnsAsync(book);

            var result = await _controller.GetBookById(bookId);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Book>(okResult.Value);
            Assert.Equal(bookId, returnValue.BookId);
        }

        [Fact]
        public async Task GetBookById_ReturnsNotFound_WhenBookDoesNotExist()
        {
            var bookId = 1;

            _mockBookService.Setup(service => service.GetByIdAsync(bookId)).ReturnsAsync((Book)null);

            var result = await _controller.GetBookById(bookId);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateBook_ReturnsCreatedAtActionResult()
        {
            var book = new Book { Name = "New Book", Year = 2023, PictureUrl = "http://example.com/newbook.jpg" };
            var createdBook = new Book { BookId = 1, Name = "New Book", Year = 2023, PictureUrl = "http://example.com/newbook.jpg" };

            _mockBookService.Setup(service => service.CreateAsync(book)).ReturnsAsync(createdBook);

            var result = await _controller.CreateBook(book);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<Book>(createdResult.Value);
            Assert.Equal(createdBook.BookId, returnValue.BookId);
        }

        [Fact]
        public async Task CreateBook_ReturnsBadRequest_WhenBookIsNull()
        {
            var result = await _controller.CreateBook(null);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task UpdateBook_ReturnsNoContent()
        {
            var bookId = 1;
            var book = new Book { BookId = bookId, Name = "Updated Book", Year = 2024, PictureUrl = "http://example.com/updatedbook.jpg" };

            _mockBookService.Setup(service => service.UpdateAsync(book)).Returns(Task.CompletedTask);

            var result = await _controller.UpdateBook(bookId, book);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateBook_ReturnsBadRequest_WhenIdMismatch()
        {
            var bookId = 1;
            var book = new Book { BookId = 2, Name = "Mismatched Book", Year = 2025, PictureUrl = "http://example.com/mismatchedbook.jpg" };

            var result = await _controller.UpdateBook(bookId, book);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteBook_ReturnsNoContent_WhenBookIsDeleted()
        {
            var bookId = 1;
            var book = new Book { BookId = bookId, Name = "Book to Delete", Year = 2022, PictureUrl = "http://example.com/booktodelete.jpg" };

            _mockBookService.Setup(service => service.GetByIdAsync(bookId)).ReturnsAsync(book);
            _mockBookService.Setup(service => service.DeleteAsync(book)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteBook(bookId);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteBook_ReturnsNotFound_WhenBookDoesNotExist()
        {
            var bookId = 1;

            _mockBookService.Setup(service => service.GetByIdAsync(bookId)).ReturnsAsync((Book)null);

            var result = await _controller.DeleteBook(bookId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task SearchBooks_ReturnsOkResult_WithListOfBooks()
        {
            var query = "Book";
            var books = new List<Book>
            {
                new Book { BookId = 1, Name = "Book One", Year = 2021, PictureUrl = "http://example.com/book1.jpg" },
                new Book { BookId = 2, Name = "Book Two", Year = 2022, PictureUrl = "http://example.com/book2.jpg" }
            };

            _mockBookService.Setup(service => service.SearchAsync(query)).ReturnsAsync(books);

            var result = await _controller.SearchBooks(query);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<Book>>(okResult.Value);
            Assert.Equal(books.Count, returnValue.Count());
        }
    }
}
