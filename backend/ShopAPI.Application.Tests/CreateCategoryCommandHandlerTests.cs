using Moq;
using ShopAPI.Application.Commands;
using ShopAPI.Application.Handlers;
using ShopAPI.Domain.Entities;
using ShopAPI.Domain.Repositories;
using FluentAssertions;



namespace ShopAPI.Application.Tests
{
    public class CreateCategoryCommandHandlerTests
    {
        private readonly Mock<ICategoryRepository> _mockRepo;
        private readonly CreateCategoryHandler _handler;

 
        public CreateCategoryCommandHandlerTests()
        {
            _mockRepo = new Mock<ICategoryRepository>();

            _handler = new CreateCategoryHandler(_mockRepo.Object);
        }

        [Fact]
        public async Task Handle_ShouldCallAddAsyncOnRepository_WhenExecuted()
        {
     
            var command = new CreateCategoryCommand
            {
                Name = "Test Category"
            };

       
            var resultId = await _handler.Handle(command, CancellationToken.None);

          
            resultId.Should().NotBeNullOrEmpty();

     
            _mockRepo.Verify(
                r => r.AddAsync(It.IsAny<Category>()),
                Times.Once
            );
        }
    }
}
