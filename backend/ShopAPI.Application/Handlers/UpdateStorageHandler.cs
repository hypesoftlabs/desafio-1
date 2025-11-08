using MediatR;
using ShopAPI.Application.Commands;
using ShopAPI.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Handlers
{
    public class UpdateStorageHandler : IRequestHandler<UpdateStorageCommand, bool>
    {
        private readonly IProductRepository _productRepository;

        public UpdateStorageHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(UpdateStorageCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId);

            if (product == null)
            {
                return false;
            }

       
            product.ChangeQuantity(request.Quantity);

         
            await _productRepository.UpdateAsync(product);

            return true;
        }
    }

}
