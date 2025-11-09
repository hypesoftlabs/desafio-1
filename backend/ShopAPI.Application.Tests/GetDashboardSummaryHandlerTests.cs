using Xunit;
using Moq;
using FluentAssertions;
using ShopAPI.Domain.Repositories;
using ShopAPI.Application.Handlers;
using ShopAPI.Application.Queries;
using ShopAPI.Domain.Entities;
using ShopAPI.Application.DTOs;
using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Text; 

namespace ShopAPI.Application.Tests
{
    public class GetDashboardSummaryHandlerTests
    {
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IDistributedCache> _mockCache;
        private readonly GetDashboardSummaryHandler _handler;

        public GetDashboardSummaryHandlerTests()
        {
            _mockRepo = new Mock<IProductRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockCache = new Mock<IDistributedCache>();

            _handler = new GetDashboardSummaryHandler(
                _mockRepo.Object,
                _mockMapper.Object,
                _mockCache.Object
            );
        }

        [Fact]
        public async Task Handle_DeveBuscarDoBancoEPreencherCache_QuandoCacheEstaVazio()
        {
            var fakeProdutoList = new List<Product> { new Product("Teste", "", 1, "cat1", 5) };
            var fakeDtoList = new List<ProductDTO> { new ProductDTO { Name = "Teste" } };

     
            _mockCache.Setup(c => c.GetAsync(
                "dashboard_summary",
                It.IsAny<CancellationToken>()))
                .ReturnsAsync((byte[])null);

            _mockRepo.Setup(r => r.GetTotalCountAsync()).ReturnsAsync(10L);
            _mockRepo.Setup(r => r.GetStorageTotalValueAsync()).ReturnsAsync(5000); 
            _mockRepo.Setup(r => r.GetLowStorageItemsAsync(10)).ReturnsAsync(fakeProdutoList);
            _mockMapper.Setup(m => m.Map<List<ProductDTO>>(fakeProdutoList)).Returns(fakeDtoList);

            var query = new GetDashboardSummaryQuery();
            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.TotalProducts.Should().Be(10L);
            result.StorageValueTotal.Should().Be(5000); 
            result.LowStorageProducts.Should().BeSameAs(fakeDtoList);

            _mockRepo.Verify(r => r.GetTotalCountAsync(), Times.Once);
            _mockRepo.Verify(r => r.GetStorageTotalValueAsync(), Times.Once);

      
            _mockCache.Verify(c => c.SetAsync(
                It.Is<string>(k => k == "dashboard_summary"),
                It.IsAny<byte[]>(),
                It.IsAny<DistributedCacheEntryOptions>(),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task Handle_DeveBuscarDoCache_QuandoCacheExiste()
        {
            var fakeSummaryDto = new DashboardSummaryDTO
            {
                TotalProducts = 99L,
                StorageValueTotal = 1234,
                LowStorageProducts = new List<ProductDTO>()
            };

       
            var fakeCachedJson = JsonSerializer.Serialize(fakeSummaryDto);
            var fakeCachedBytes = Encoding.UTF8.GetBytes(fakeCachedJson);

         
            _mockCache.Setup(c => c.GetAsync(
                "dashboard_summary",
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(fakeCachedBytes);

            var query = new GetDashboardSummaryQuery();
            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.TotalProducts.Should().Be(99L);
            result.StorageValueTotal.Should().Be(1234);

            _mockRepo.Verify(r => r.GetTotalCountAsync(), Times.Never);
            _mockRepo.Verify(r => r.GetStorageTotalValueAsync(), Times.Never);
            _mockRepo.Verify(r => r.GetLowStorageItemsAsync(It.IsAny<int>()), Times.Never);
        }
    }
}