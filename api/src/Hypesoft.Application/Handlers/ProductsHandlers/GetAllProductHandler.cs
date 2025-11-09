using AutoMapper;
using Hypesoft.Application.Dtos;
using Hypesoft.Application.Queries;
using Hypesoft.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypesoft.Application.Handlers.ProductsHandlers
{
    public class GetAllProductHandler : IRequestHandler<GetAllProductQuery, IEnumerable<ProductDto>>
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetAllProductHandler(IProductRepository repository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllAsync();

            
            foreach (var p in products)
            {
                if (p.CategoryId != null && p.Category == null)
                {
                    p.Category = await _categoryRepository.GetByIdAsync(p.CategoryId);
                }
            }

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
    }
}