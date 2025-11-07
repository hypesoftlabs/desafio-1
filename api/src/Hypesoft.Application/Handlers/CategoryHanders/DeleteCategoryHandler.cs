using AutoMapper;
using Hypesoft.Application.Commands.CategoryCommand;
using Hypesoft.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypesoft.Application.Handlers.CategoryHanders
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;   

        public DeleteCategoryHandler(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var existingCategory = await _repository.GetByIdAsync(request.Id);
            if (existingCategory is null)
                throw new KeyNotFoundException($"Categoria com ID '{request.Id}' não encontrado.");

            await _repository.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
