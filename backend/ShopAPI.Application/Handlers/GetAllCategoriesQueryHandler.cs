using MediatR;
using ShopAPI.Application.Queries;
using ShopAPI.Domain.Entities;
using ShopAPI.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Handlers
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<Category>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetAllCategoriesQueryHandler(ICategoryRepository categoriaRepository)
        {
            _categoryRepository = categoriaRepository;
        }

        public async Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            // Apenas chamamos o repositório
            return await _categoryRepository.GetAllAsync();
        }
    }
}
