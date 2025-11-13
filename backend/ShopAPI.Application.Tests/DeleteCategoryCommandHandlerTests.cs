using Moq;
using ShopAPI.Application.Commands;
using ShopAPI.Application.Handlers;
using ShopAPI.Domain.Entities;
using ShopAPI.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace ShopAPI.Application.Tests
{
    public class DeleteCategoryCommandHandlerIRequestHandlerTests
    {
        private readonly Mock<ICategoryRepository> _mockCategoryRepo;
        private readonly Mock<IProductRepository> _mockProductRepo; // We need both repos
        private readonly DeleteCategoryCommandHandlerIRequestHandler _handler;
        private readonly Category _existingCategory;

        public DeleteCategoryCommandHandlerIRequestHandlerTests()
        {
            _mockCategoryRepo = new Mock<ICategoryRepository>();
            _mockProductRepo = new Mock<IProductRepository>();

    
            _existingCategory = new Category("Test Category");


            _mockCategoryRepo.Setup(r => r.GetByIdAsync(_existingCategory.Id))
                             .ReturnsAsync(_existingCategory);

     
            _handler = new DeleteCategoryCommandHandlerIRequestHandler(
                _mockCategoryRepo.Object,
                _mockProductRepo.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldReturnTrueAndCallDelete_WhenCategoryExistsAndIsNotInUse()
        {
           
            _mockProductRepo.Setup(r => r.HasProductsInTheCategory(_existingCategory.Id))
                            .ReturnsAsync(false);

         
            var command = new DeleteCategoryCommand(_existingCategory.Id);

      
            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().BeTrue();

            _mockCategoryRepo.Verify(r => r.GetByIdAsync(_existingCategory.Id), Times.Once);
            _mockProductRepo.Verify(r => r.HasProductsInTheCategory(_existingCategory.Id), Times.Once);
   
            _mockCategoryRepo.Verify(r => r.DeleteAsync(_existingCategory.Id), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenCategoryDoesNotExist()
        {
       
            var command = new DeleteCategoryCommand("non-existing-id");

      
            _mockCategoryRepo.Setup(r => r.GetByIdAsync("non-existing-id"))
                             .ReturnsAsync((Category)null);

         
            var result = await _handler.Handle(command, CancellationToken.None);

         
            result.Should().BeFalse();


            _mockCategoryRepo.Verify(r => r.GetByIdAsync("non-existing-id"), Times.Once);

       
            _mockProductRepo.Verify(r => r.HasProductsInTheCategory(It.IsAny<string>()), Times.Never);
            _mockCategoryRepo.Verify(r => r.DeleteAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenCategoryIsInUse()
        {

            _mockProductRepo.Setup(r => r.HasProductsInTheCategory(_existingCategory.Id))
                            .ReturnsAsync(true);

 
            var command = new DeleteCategoryCommand(_existingCategory.Id);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().BeFalse();

            _mockCategoryRepo.Verify(r => r.GetByIdAsync(_existingCategory.Id), Times.Once);

            _mockProductRepo.Verify(r => r.HasProductsInTheCategory(_existingCategory.Id), Times.Once);

            _mockCategoryRepo.Verify(r => r.DeleteAsync(It.IsAny<string>()), Times.Never);
        }
    }
}
