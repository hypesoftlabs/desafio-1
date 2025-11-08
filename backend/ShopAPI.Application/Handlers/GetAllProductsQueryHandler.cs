using AutoMapper;
using MediatR;
using ShopAPI.Application.Common;
using ShopAPI.Application.DTOs;
using ShopAPI.Application.Queries;
using ShopAPI.Domain.Repositories;


namespace ShopAPI.Application.Handlers
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Pagination<ProductDTO>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IProductRepository produtoRepository, IMapper mapper)
        {
            _productRepository = produtoRepository;
            _mapper = mapper;

        }

        public async Task<Pagination<ProductDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            
            var products = await _productRepository.GetAllAsync(request.Name, request.CategoryId, request.PageNumber,request.PageSize);

            var productsDTO = _mapper.Map<List<ProductDTO>>(products.Data);

            var paginatedResult = new Pagination<ProductDTO>(
                productsDTO,
                products.TotalCount,
                products.PageNumber, 
                products.PageSize  
            );

            return paginatedResult;
        }
    }
}
