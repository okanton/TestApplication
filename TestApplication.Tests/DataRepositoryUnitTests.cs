using AutoFixture;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApplication.Model;
using TestApplication.Services;
using Xunit;

namespace TestApplication.Tests
{
    public class DataRepositoryUnitTests
    {
        private readonly Mock<IHttpService> _mockHttpService;
        private readonly IDataRepository _repository;

        private const int userId = 2;

        public DataRepositoryUnitTests()
        {
            _mockHttpService = new Mock<IHttpService>();
            var fixture = new Fixture();

            var mockPosts = fixture.Build<Post>().CreateMany(10);
            var mockTodos = fixture.Build<Todo>().CreateMany(10);
            var mockUser = fixture.Build<User>().With(p => p.Id, 2).Create();
            _mockHttpService.Setup(x => x.GetDataFromAPIAsync<User>($"users/{userId}")).Returns(Task.FromResult(mockUser));
            _mockHttpService.Setup(x => x.GetDataFromAPIAsync<IEnumerable<Post>>($"posts?userId={userId}")).Returns(Task.FromResult(mockPosts));
            _mockHttpService.Setup(x => x.GetDataFromAPIAsync<IEnumerable<Todo>>($"todos?userId={userId}")).Returns(Task.FromResult(mockTodos));
            _repository = new DataRepository(_mockHttpService.Object);
        }

        [Fact]
        public async Task GetUserByIdAsync_ByUserId_ReturnUserObject()
        {
            // Arrange

            // Act
            var result = await _repository.GetUserByIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(result.Id, userId);
            Assert.True(result.Posts.Any());
            Assert.True(result.Todos.Any());
        }

        [Fact]
        public async Task GetUserByIdAsync_ByUserId_ReturnException()
        {
            // Arrange
            var errorUserId = 22;

            // Act
            var ex = await Assert.ThrowsAsync<Exception>(() => _repository.GetUserByIdAsync(errorUserId));

            // Assert
            Assert.Equal("Пользователя с таким ID не существует", ex.Message);
        }
    }
}