using MediatR;
using ShopAPI.Application.Commands;
using ShopAPI.Domain.Entities;
using ShopAPI.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Handlers
{
    public class CreateProductCommandHandler(IProductRepository produtoRepository) : IRequestHandler<CreateProductCommand, string>
    {
        private readonly IProductRepository _productRepository = produtoRepository;

        public async Task<string> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var produto = new Product(
                request.Name,
                request.Description,
                request.Price,
                request.CategoryId, 
                request.Quantity
            );
      
            await _productRepository.AddAsync(produto);

            return produto.Id;
        }
    }

}
