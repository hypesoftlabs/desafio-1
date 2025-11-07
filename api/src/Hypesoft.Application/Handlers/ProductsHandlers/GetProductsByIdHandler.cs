using AutoMapper;
using Hypesoft.Application.Dtos;
using Hypesoft.Application.Queries;
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
    public class GetProductsByIdHandler : IRequestHandler<GetProductsIdQuery,ProductDto>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public GetProductsByIdHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(GetProductsIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);
            return product is not null ? _mapper.Map<ProductDto>(product) : null;
        }
    }
}
