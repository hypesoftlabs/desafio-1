using MediatR;
using ShopAPI.Application.Commands;
using ShopAPI.Domain.Entities;
using ShopAPI.Domain.Repositories;


namespace ShopAPI.Application.Handlers
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, string>
    {
        private readonly ICategoryRepository _categoriaRepository;

        public CreateCategoryHandler(ICategoryRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<string> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
        
            var categoria = new Category(request.Name);

            await _categoriaRepository.AddAsync(categoria);

            return categoria.Id;
        }
    }
}
