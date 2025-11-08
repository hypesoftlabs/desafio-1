using AutoMapper;
using MediatR;
using ShopAPI.Application.DTOs;
using ShopAPI.Application.Queries;
using ShopAPI.Domain.Entities;
using ShopAPI.Domain.Repositories;


namespace ShopAPI.Application.Handlers
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDTO>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IProductRepository produtoRepository, IMapper mapper)
        {
            _productRepository = produtoRepository;
            _mapper = mapper;

        }

        public async Task<IEnumerable<ProductDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            
            var produtos = await _productRepository.GetAllAsync();

            var produtosDto = _mapper.Map<IEnumerable<ProductDTO>>(produtos);

            return produtosDto;
        }
    }
}
