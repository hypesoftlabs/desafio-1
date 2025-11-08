using AutoMapper;
using MediatR;
using ShopAPI.Application.DTOs;
using ShopAPI.Application.Queries;
using ShopAPI.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Handlers
{
    public class GetDashboardSummaryHandler : IRequestHandler<GetDashboardSummaryQuery, DashboardSummaryDTO>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        private const int STOCK_LIMIT = 10;

        public GetDashboardSummaryHandler(IProductRepository produtoRepository, IMapper mapper)
        {
            _productRepository = produtoRepository;
            _mapper = mapper;
        }

      
        public async Task<DashboardSummaryDTO> Handle(GetDashboardSummaryQuery request, CancellationToken cancellationToken)
        {
           
            var totalProducts = await _productRepository.GetTotalCountAsync();
            var valueStorage = await _productRepository.GetStorageTotalValueAsync();
            var lowStorageProducts = await _productRepository.GetLowStorageItemsAsync(STOCK_LIMIT);

            var productsDTO = _mapper.Map<List<ProductDTO>>(lowStorageProducts);


            return new DashboardSummaryDTO
            {
                TotalProducts = totalProducts,
                StorageValueTotal = valueStorage,
                LowStorageProducts = productsDTO
            };
        }
    }
}
