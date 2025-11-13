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
    public class EditProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly EditProductHandler _handler;
        private readonly Product _existingProduct;
        private readonly EditProductCommand _command;

        public EditProductCommandHandlerTests()
        {
            _mockRepo = new Mock<IProductRepository>();

      
            _existingProduct = new Product(
                "Original Name",
                "Original Desc",
                100,
                "cat-123",
                10
            );

        
            _command = new EditProductCommand
            {
                Id = _existingProduct.Id,
                Name = "Updated Name",
                Description = "Updated Desc",
                Price = 150,
                CategoryId = "cat-456",
                Quantity = 5
            };

 
            _mockRepo.Setup(r => r.GetByIdAsync(_existingProduct.Id))
                     .ReturnsAsync(_existingProduct);

            _handler = new EditProductHandler(_mockRepo.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnTrue_WhenProductExists()
        {
           
            var result = await _handler.Handle(_command, CancellationToken.None);

         
            result.Should().BeTrue();

            _existingProduct.Name.Should().Be("Updated Name");
            _existingProduct.Price.Should().Be(150);
            _existingProduct.Quantity.Should().Be(5);


            _mockRepo.Verify(
                r => r.UpdateAsync(_existingProduct),
                Times.Once
            );
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenProductDoesNotExist()
        {
          
            var badCommand = new EditProductCommand { Id = "non-existing-id" };

     
            var result = await _handler.Handle(badCommand, CancellationToken.None);

            result.Should().BeFalse();

       
            _mockRepo.Verify(
                r => r.UpdateAsync(It.IsAny<Product>()),
                Times.Never
            );
        }
    }

}
