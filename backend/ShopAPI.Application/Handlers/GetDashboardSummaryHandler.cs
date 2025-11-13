using AutoMapper;
using MediatR;
using ShopAPI.Application.DTOs;
using ShopAPI.Application.Queries;
using ShopAPI.Domain.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;


namespace ShopAPI.Application.Handlers
{
    public class GetDashboardSummaryHandler : IRequestHandler<GetDashboardSummaryQuery, DashboardSummaryDTO>
    {
        private readonly IProductRepository _productRepository;
        private readonly IDistributedCache _cache;
        private readonly IMapper _mapper;

        private const int STOCK_LIMIT = 10;

        public GetDashboardSummaryHandler(
            IProductRepository produtoRepository,
            IMapper mapper,
            IDistributedCache cache)
        {
            _productRepository = produtoRepository;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<DashboardSummaryDTO> Handle(GetDashboardSummaryQuery request, CancellationToken cancellationToken)
        {
            const string cacheKey = "dashboard_summary";
            DashboardSummaryDTO summary;

            var cachedSummary = await _cache.GetStringAsync(cacheKey, cancellationToken);

            if (!string.IsNullOrEmpty(cachedSummary))
            {
                summary = JsonSerializer.Deserialize<DashboardSummaryDTO>(cachedSummary);
            }
            else
            {
                var totalProducts = await _productRepository.GetTotalCountAsync();
                var valueStorage = await _productRepository.GetStorageTotalValueAsync();
                var lowStorageProducts = await _productRepository.GetLowStorageItemsAsync(STOCK_LIMIT);

                var productsDTO = _mapper.Map<List<ProductDTO>>(lowStorageProducts);

                summary = new DashboardSummaryDTO
                {
                    TotalProducts = totalProducts,
                    StorageValueTotal = valueStorage,
                    LowStorageProducts = productsDTO
                };

                var summaryJson = JsonSerializer.Serialize(summary);
                var cacheOptions = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                };

                await _cache.SetStringAsync(cacheKey, summaryJson, cacheOptions, cancellationToken);
            }

            return summary;
        }
    }
}