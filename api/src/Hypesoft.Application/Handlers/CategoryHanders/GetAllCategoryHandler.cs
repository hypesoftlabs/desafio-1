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

namespace Hypesoft.Application.Handlers.CategoryHanders
{
    public class GetAllCategoryHandler : IRequestHandler<GetAllCategoryQuery, IEnumerable<CategoryDto>>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        public GetAllCategoryHandler(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(products);
        }
    }
}
