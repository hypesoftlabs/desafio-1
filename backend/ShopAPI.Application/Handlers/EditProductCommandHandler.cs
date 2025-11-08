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
    public class EditProductCommandHandler : IRequestHandler<EditProductCommand, bool>
    {
        private readonly IProductRepository _produtoRepository;

     
        public EditProductCommandHandler(IProductRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

    
        public async Task<bool> Handle(EditProductCommand request, CancellationToken cancellationToken)
        {
          
            var produto = await _produtoRepository.GetByIdAsync(request.Id);

            if (produto == null)
            {
                return false;
            }

        
            produto.ChangeName(request.Name);
            produto.ChangeDescription(request.Description);
            produto.ChangePrice(request.Price);
            produto.ChangeCategory(request.CategoryId);
            produto.ChangeQuantity(request.Quantity);

 
            await _produtoRepository.UpdateAsync(produto);

            return true;
        }
    }
}
