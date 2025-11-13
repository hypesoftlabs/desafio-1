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
    public class DeleteCategoryCommandHandlerIRequestHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
      
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public DeleteCategoryCommandHandlerIRequestHandler(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
           
            var category = await _categoryRepository.GetByIdAsync(request.Id);
            if (category == null)
            {
                return false; 
            }

          
            var isUsed = await _productRepository.HasProductsInTheCategory(request.Id);

            if (isUsed)
            {
              
                return false;
            }

   
            await _categoryRepository.DeleteAsync(request.Id);

            return true; 
        }
    }
}
