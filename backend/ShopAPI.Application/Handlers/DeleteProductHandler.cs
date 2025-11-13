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
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository _produtoRepository;

        public DeleteProductHandler(IProductRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
         
            var produto = await _produtoRepository.GetByIdAsync(request.Id);

            if (produto == null)
            {
                return false;
            }
       
            await _produtoRepository.DeleteAsync(produto.Id);

            return true;
        }
    }

}
