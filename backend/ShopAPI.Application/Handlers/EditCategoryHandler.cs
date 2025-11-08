using MediatR;
using ShopAPI.Application.Commands;
using ShopAPI.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Handlers
{
    public class EditCategoryHandler : IRequestHandler<EditCategoryCommand, bool>
    {
        private readonly ICategoryRepository _categoryRepository;

        public EditCategoryHandler(ICategoryRepository categoriaRepository)
        {
            _categoryRepository = categoriaRepository;
        }

        public async Task<bool> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
        {
           
            var category = await _categoryRepository.GetByIdAsync(request.Id);

            if (category == null)
            {
                return false;
            }

            category.UpdateName(request.Name);

            await _categoryRepository.UpdateAsync(category);


            return true;
        }
    }
}
