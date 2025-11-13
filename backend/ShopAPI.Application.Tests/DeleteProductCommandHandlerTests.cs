using Moq;
using ShopAPI.Application.Commands;
using ShopAPI.Application.Handlers;
using ShopAPI.Domain.Entities;
using ShopAPI.Domain.Repositories;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Tests
{
    public class DeleteProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly DeleteProductHandler _handler;
        private readonly Product _existingProduct;

   
        public DeleteProductCommandHandlerTests()
        {
            _mockRepo = new Mock<IProductRepository>();

        
            _existingProduct = new Product(
                "Product To Delete",
                "Desc",
                100,
                "cat-123",
                10
            );

            // 2. Setup the mock repo to find this product
            _mockRepo.Setup(r => r.GetByIdAsync(_existingProduct.Id))
                     .ReturnsAsync(_existingProduct);

            // 3. Create the handler
            _handler = new DeleteProductHandler(_mockRepo.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnTrueAndCallDelete_WhenProductExists()
        {
       
            var command = new DeleteProductCommand(_existingProduct.Id);

      
            var result = await _handler.Handle(command, CancellationToken.None);

       
            result.Should().BeTrue();

       
            _mockRepo.Verify(
                r => r.DeleteAsync(_existingProduct.Id),
                Times.Once
            );
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenProductDoesNotExist()
        {
            
            var command = new DeleteProductCommand("non-existing-id");

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().BeFalse();

            _mockRepo.Verify(
                r => r.DeleteAsync(It.IsAny<string>()),
                Times.Never
            );
        }
    }
}
