using AutoMapper;
using Hypesoft.Application.Commands.CategoryCommand;
using Hypesoft.Application.Dtos;
using Hypesoft.Domain.Entity;
using Hypesoft.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypesoft.Application.Handlers.CategoryHanders
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CreateCategoryHandler(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Category>(request.Category);
            await _repository.AddAsync(entity);
            return _mapper.Map<CategoryDto>(entity);
        }
    }
}
