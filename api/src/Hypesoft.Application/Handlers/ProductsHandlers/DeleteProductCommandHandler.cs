using Hypesoft.Application.Commands.ProductsCommand;
using Hypesoft.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypesoft.Application.Handlers.ProductsHandlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IProductRepository _repository;

        public DeleteProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var existingProduct = await _repository.GetByIdAsync(request.Id);
            if (existingProduct is null)
                throw new KeyNotFoundException($"Produto com ID '{request.Id}' não encontrado.");

            await _repository.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
