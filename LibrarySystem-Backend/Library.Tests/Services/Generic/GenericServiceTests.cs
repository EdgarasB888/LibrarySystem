using Library.Core.Services.Generic;
using Library.Infrastructure.Repositories.Generic;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.Services.Generic
{
    public class GenericServiceTests
    {
        private readonly Mock<IGenericRepository<string>> _mockRepository;
        private readonly GenericService<string> _service;

        public GenericServiceTests()
        {
            _mockRepository = new Mock<IGenericRepository<string>>();
            _service = new GenericService<string>(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllItems()
        {
            var items = new List<string> { "item1", "item2" };
            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(items);

            var result = await _service.GetAllAsync();

            Assert.Equal(items.Count, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsItem()
        {
            var item = "item1";
            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(item);

            var result = await _service.GetByIdAsync(1);

            Assert.Equal(item, result);
        }

        [Fact]
        public async Task CreateAsync_AddsItem()
        {
            var item = "item1";
            _mockRepository.Setup(repo => repo.AddAsync(item)).Returns(Task.CompletedTask);

            var result = await _service.CreateAsync(item);

            Assert.Equal(item, result);
            _mockRepository.Verify(repo => repo.AddAsync(item), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesItem()
        {
            var item = "item1";
            _mockRepository.Setup(repo => repo.UpdateAsync(item)).Returns(Task.CompletedTask);

            await _service.UpdateAsync(item);

            _mockRepository.Verify(repo => repo.UpdateAsync(item), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_DeletesItem()
        {
            var item = "item1";
            _mockRepository.Setup(repo => repo.DeleteAsync(item)).Returns(Task.CompletedTask);

            await _service.DeleteAsync(item);

            _mockRepository.Verify(repo => repo.DeleteAsync(item), Times.Once);
        }
    }
}
