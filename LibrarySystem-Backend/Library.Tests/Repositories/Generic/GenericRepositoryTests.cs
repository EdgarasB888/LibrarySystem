using Library.Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.Repositories.Generic
{
    public class GenericRepositoryTests
    {
        private class TestEntity
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        private class TestDbContext : DbContext
        {
            public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }
            public DbSet<TestEntity> TestEntities { get; set; }
        }

        private GenericRepository<TestEntity> GetRepository(TestDbContext context)
        {
            return new GenericRepository<TestEntity>(context);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllEntities()
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllAsyncTest")
                .Options;

            using (var context = new TestDbContext(options))
            {
                context.TestEntities.AddRange(
                    new TestEntity { Id = 1, Name = "Entity One" },
                    new TestEntity { Id = 2, Name = "Entity Two" }
                );
                context.SaveChanges();
            }

            using (var context = new TestDbContext(options))
            {
                var repository = GetRepository(context);
                var result = await repository.GetAllAsync();

                Assert.Equal(2, ((List<TestEntity>)result).Count);
            }
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectEntity()
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: "GetByIdAsyncTest")
                .Options;

            using (var context = new TestDbContext(options))
            {
                context.TestEntities.Add(new TestEntity { Id = 1, Name = "Entity One" });
                context.SaveChanges();
            }

            using (var context = new TestDbContext(options))
            {
                var repository = GetRepository(context);
                var result = await repository.GetByIdAsync(1);

                Assert.NotNull(result);
                Assert.Equal(1, result.Id);
            }
        }

        [Fact]
        public async Task AddAsync_AddsNewEntityToDatabase()
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: "AddAsyncTest")
                .Options;

            using (var context = new TestDbContext(options))
            {
                var repository = GetRepository(context);

                await repository.AddAsync(new TestEntity { Id = 1, Name = "New Entity" });
            }

            using (var context = new TestDbContext(options))
            {
                var entity = await context.TestEntities.FindAsync(1);
                Assert.NotNull(entity);
                Assert.Equal("New Entity", entity.Name);
            }
        }

        [Fact]
        public async Task UpdateAsync_UpdatesExistingEntity()
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: "UpdateAsyncTest")
                .Options;

            using (var context = new TestDbContext(options))
            {
                context.TestEntities.Add(new TestEntity { Id = 1, Name = "Old Name" });
                context.SaveChanges();
            }

            using (var context = new TestDbContext(options))
            {
                var repository = GetRepository(context);
                var entity = await context.TestEntities.FindAsync(1);
                entity.Name = "Updated Name";
                await repository.UpdateAsync(entity);
            }

            using (var context = new TestDbContext(options))
            {
                var entity = await context.TestEntities.FindAsync(1);
                Assert.NotNull(entity);
                Assert.Equal("Updated Name", entity.Name);
            }
        }

        [Fact]
        public async Task DeleteAsync_RemovesEntityFromDatabase()
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteAsyncTest")
                .Options;

            using (var context = new TestDbContext(options))
            {
                context.TestEntities.Add(new TestEntity { Id = 1, Name = "Entity to Delete" });
                context.SaveChanges();
            }

            using (var context = new TestDbContext(options))
            {
                var repository = GetRepository(context);
                var entity = await repository.GetByIdAsync(1);
                await repository.DeleteAsync(entity);
            }

            using (var context = new TestDbContext(options))
            {
                var entity = await context.TestEntities.FindAsync(1);
                Assert.Null(entity);
            }
        }
    }
}
