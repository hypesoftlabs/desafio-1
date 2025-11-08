using MediatR;
using ShopAPI.Application.Queries;
using ShopAPI.Domain.Entities;
using ShopAPI.Domain.Repositories;


namespace ShopAPI.Application.Handlers
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsQueryHandler(IProductRepository produtoRepository)
        {
            _productRepository = produtoRepository;

        }

        public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetAllAsync();
        }
    }
}
