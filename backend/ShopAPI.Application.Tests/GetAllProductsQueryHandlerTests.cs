using AutoMapper;
using Moq;
using ShopAPI.Application.Common;
using ShopAPI.Application.DTOs;
using ShopAPI.Application.Handlers;
using ShopAPI.Application.Queries;
using ShopAPI.Domain.Entities;
using ShopAPI.Domain.Repositories;
using FluentAssertions;


namespace ShopAPI.Application.Tests
{
    public class GetAllProductsQueryHandlerTests
    {
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetAllProductsHandler _handler;

        public GetAllProductsQueryHandlerTests()
        {
            _mockRepo = new Mock<IProductRepository>();
            _mockMapper = new Mock<IMapper>();

            _handler = new GetAllProductsHandler(
                _mockRepo.Object,
                _mockMapper.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldReturnPaginatedDtoResult_WhenCalled()
        {
            
            var query = new GetAllProductsQuery
            {
                Name = "test",
                CategoryId = "cat-123",
                PageNumber = 1,
                PageSize = 10
            };

          
            var fakeProductList = new List<Product>
            {
                new Product("Test Product", "Desc", 100, "cat-123", 5)
            };
            var paginatedResult = new Pagination<Product>(
                fakeProductList,
                1, 
                1, 
                10 
            );

         
            var fakeDtoList = new List<ProductDTO>
            {
                new ProductDTO { Id = "prod-1", Name = "Test Product" }
            };

 
            _mockRepo.Setup(r => r.GetAllAsync(
                query.Name,
                query.CategoryId,
                query.PageNumber,
                query.PageSize))
                .ReturnsAsync(paginatedResult);

            _mockMapper.Setup(m => m.Map<List<ProductDTO>>(fakeProductList))
                       .Returns(fakeDtoList);

    
            var result = await _handler.Handle(query, CancellationToken.None);


            result.Should().NotBeNull();
            result.Should().BeOfType<Pagination<ProductDTO>>();

         
            result.TotalCount.Should().Be(1);
            result.PageNumber.Should().Be(1);
            result.Data.Should().BeSameAs(fakeDtoList); 

         
            _mockRepo.Verify(r => r.GetAllAsync(
                query.Name,
                query.CategoryId,
                query.PageNumber,
                query.PageSize), Times.Once);

            _mockMapper.Verify(m => m.Map<List<ProductDTO>>(fakeProductList), Times.Once);
        }
    }
}
