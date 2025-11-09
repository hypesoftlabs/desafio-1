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

   
    public class GetCategoryIdHandler : IRequestHandler<GetCategoryIdQuery, CategoryDto>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public GetCategoryIdHandler(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(GetCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var categories = await _repository.GetByIdAsync(request.Id);
            return categories is not null ? _mapper.Map<CategoryDto>(categories) : null;
        }
    }
}
