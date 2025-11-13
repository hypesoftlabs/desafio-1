using Moq;
using ShopAPI.Application.Commands;
using ShopAPI.Application.Handlers;
using ShopAPI.Domain.Entities;
using ShopAPI.Domain.Repositories;
using FluentAssertions;


namespace ShopAPI.Application.Tests
{
    public class UpdateStorageHandlerTests
    {
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly UpdateStorageHandler _handler;
        private readonly Product _existingProduct;
        private readonly UpdateStorageCommand _command;

        // Setup
        public UpdateStorageHandlerTests()
        {
            _mockRepo = new Mock<IProductRepository>();

  
            _existingProduct = new Product(
                "Test Product",
                "Desc",
                100,
                "cat-123",
                10 
            );

            _command = new UpdateStorageCommand
            {
                ProductId = _existingProduct.Id,
                Quantity = 25 
            };

        
            _mockRepo.Setup(r => r.GetByIdAsync(_existingProduct.Id))
                     .ReturnsAsync(_existingProduct);

            _handler = new UpdateStorageHandler(_mockRepo.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnTrue_WhenProductExists()
        {

            var result = await _handler.Handle(_command, CancellationToken.None);

     
            result.Should().BeTrue();

   
            _existingProduct.Quantity.Should().Be(25);

            _mockRepo.Verify(
                r => r.UpdateAsync(_existingProduct),
                Times.Once
            );
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenProductDoesNotExist()
        {
        
            var badCommand = new UpdateStorageCommand
            {
                ProductId = "non-existing-id",
                Quantity = 20
            };


            var result = await _handler.Handle(badCommand, CancellationToken.None);


            result.Should().BeFalse();

  
            _mockRepo.Verify(
                r => r.UpdateAsync(It.IsAny<Product>()),
                Times.Never
            );
        }
    }
}
