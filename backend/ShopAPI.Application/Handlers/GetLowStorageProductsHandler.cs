using AutoMapper;
using MediatR;
using ShopAPI.Application.Common;
using ShopAPI.Application.DTOs;
using ShopAPI.Application.Queries;
using ShopAPI.Domain.Repositories;


namespace ShopAPI.Application.Handlers
{
    public class GetLowStorageProductsHandler : IRequestHandler<GetLowStorageProductsQuery, Pagination<ProductDTO>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        private const int LOW_STORAGE_LIMIT = 10;

        public GetLowStorageProductsHandler(IProductRepository produtoRepository, IMapper mapper)
        {
            _productRepository = produtoRepository;
            _mapper = mapper;

        }

        public async Task<Pagination<ProductDTO>> Handle(GetLowStorageProductsQuery request, CancellationToken cancellationToken)
        {
            
            var paginatedProdutos = await _productRepository.GetLowStorageItemsAsync(
                LOW_STORAGE_LIMIT, 
                request.PageNumber,
                request.PageSize
            );

         
            var productsDto = _mapper.Map<List<ProductDTO>>(paginatedProdutos.Data);

      
            return new Pagination<ProductDTO>(
                productsDto,
                paginatedProdutos.TotalCount,
                paginatedProdutos.PageNumber,
                paginatedProdutos.PageSize
            );
        }
    }

}
