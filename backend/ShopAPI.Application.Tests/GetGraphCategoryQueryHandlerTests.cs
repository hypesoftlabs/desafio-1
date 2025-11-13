using Xunit;
using Moq;
using FluentAssertions;
using ShopAPI.Domain.Repositories;
using ShopAPI.Application.Handlers;
using ShopAPI.Application.Queries;
using ShopAPI.Domain.Entities;
using ShopAPI.Application.DTOs; // For GraphCategoryDto
using System.Collections.Generic;
using System.Linq; // For .First()
using System.Threading;
using System.Threading.Tasks;

// Assuming English naming conventions

namespace ShopAPI.Application.Tests
{
    public class GetGraphCategoryQueryHandlerTests
    {
        private readonly Mock<IProductRepository> _mockProductRepo;
        private readonly Mock<ICategoryRepository> _mockCategoryRepo;
        private readonly GetGraphCategoryHandler _handler;


        public GetGraphCategoryQueryHandlerTests()
        {
            _mockProductRepo = new Mock<IProductRepository>();
            _mockCategoryRepo = new Mock<ICategoryRepository>();

            _handler = new GetGraphCategoryHandler(
                _mockProductRepo.Object,
                _mockCategoryRepo.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldReturnCorrectProductCountsPerCategory()
        {
         
            var category1 = new Category("Electronics"); 
            var category2 = new Category("Books");       
            var fakeCategories = new List<Category> { category1, category2 };

 
            var fakeProducts = new List<Product>
            {
         
                new Product("TV", "", 100, category1.Id, 5),
                new Product("Laptop", "", 100, category1.Id, 10),
               
                new Product("DDD Book", "", 100, category2.Id, 20)
            };


            _mockProductRepo.Setup(r => r.GetFullListAsync())
                            .ReturnsAsync(fakeProducts);

    
            _mockCategoryRepo.Setup(r => r.GetAllAsync())
                             .ReturnsAsync(fakeCategories);

            
            var query = new GetGraphCategoryQuery();

        
            var result = await _handler.Handle(query, CancellationToken.None);


            result.Should().NotBeNull();
            result.Should().HaveCount(2); 

      
            var electronics = result.First(r => r.CategoryName == "Electronics");
            var books = result.First(r => r.CategoryName == "Books");

            electronics.ProductQuantity.Should().Be(2); 
            books.ProductQuantity.Should().Be(1);      

     
            _mockProductRepo.Verify(r => r.GetFullListAsync(), Times.Once);
            _mockCategoryRepo.Verify(r => r.GetAllAsync(), Times.Once);
        }
    }
}