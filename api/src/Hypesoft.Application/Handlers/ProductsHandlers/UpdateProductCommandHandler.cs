using AutoMapper;
using Hypesoft.Application.Commands.ProductsCommand;
using Hypesoft.Application.Dtos;
using Hypesoft.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypesoft.Application.Handlers.ProductsHandlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var existingProduct = await _repository.GetByIdAsync(request.Id);
            if (existingProduct is null)
                throw new KeyNotFoundException($"Produto com ID '{request.Id}' não encontrado.");

            existingProduct.Name = request.Name;
            existingProduct.Description = request.Description;
            existingProduct.Price = request.Price;
            existingProduct.CategoryId = request.CategoryId;
            existingProduct.StockQuantity = request.StockQuantity;

            await _repository.UpdateAsync(existingProduct);

            return _mapper.Map<ProductDto>(existingProduct);
        }
    }
}
