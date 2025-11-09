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
    public class EditCategoryCommandHandlerTests
    {
        private readonly Mock<ICategoryRepository> _mockRepo;
        private readonly EditCategoryHandler _handler;
        private readonly Category _existingCategory;
        private readonly EditCategoryCommand _command;

        // Setup
        public EditCategoryCommandHandlerTests()
        {
            _mockRepo = new Mock<ICategoryRepository>();
         
            _existingCategory = new Category("Original Name");

            _command = new EditCategoryCommand
            {
                Id = _existingCategory.Id,
                Name = "Updated Name"
            };

   
            _mockRepo.Setup(r => r.GetByIdAsync(_existingCategory.Id))
                     .ReturnsAsync(_existingCategory);

       
            _handler = new EditCategoryHandler(_mockRepo.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnTrue_WhenCategoryExists()
        {

            var result = await _handler.Handle(_command, CancellationToken.None);

            result.Should().BeTrue();

 
            _existingCategory.Name.Should().Be("Updated Name");

            _mockRepo.Verify(
                r => r.UpdateAsync(_existingCategory),
                Times.Once
            );
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenCategoryDoesNotExist()
        {

            var badCommand = new EditCategoryCommand
            {
                Id = "non-existing-id",
                Name = "Test"
            };

            var result = await _handler.Handle(badCommand, CancellationToken.None);

            result.Should().BeFalse();

            _mockRepo.Verify(
                r => r.UpdateAsync(It.IsAny<Category>()),
                Times.Never
            );
        }
    }
}
