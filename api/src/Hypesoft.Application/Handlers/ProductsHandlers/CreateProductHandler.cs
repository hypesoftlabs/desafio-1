using AutoMapper;
using Hypesoft.Application.Commands.ProductsCommand;
using Hypesoft.Application.Dtos;
using Hypesoft.Domain.Entity;
using Hypesoft.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypesoft.Application.Handlers.ProductsHandlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public CreateProductHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Product>(request.Product);
            await _repository.AddAsync(entity);
            return _mapper.Map<ProductDto>(entity);
        }
    }
}
