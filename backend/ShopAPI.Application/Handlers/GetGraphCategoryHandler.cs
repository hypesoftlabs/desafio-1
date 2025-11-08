using MediatR;
using ShopAPI.Application.DTOs;
using ShopAPI.Application.Queries;
using ShopAPI.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Handlers
{
    public class GetGraphCategoryHandler : IRequestHandler<GetGraphCategoryQuery, IEnumerable<GraphCategoryDTO>>
        {
            private readonly IProductRepository _produtoRepository;
            private readonly ICategoryRepository _categoriaRepository;

            public GetGraphCategoryHandler(IProductRepository produtoRepository, ICategoryRepository categoriaRepository)
            {
                _produtoRepository = produtoRepository;
                _categoriaRepository = categoriaRepository;
            }

            public async Task<IEnumerable<GraphCategoryDTO>> Handle(GetGraphCategoryQuery request, CancellationToken cancellationToken)
            {
             
                var allProducts = await _produtoRepository.GetFullListAsync();

      
                var allCategories = await _categoriaRepository.GetAllAsync(); 

        
                var countByCategory = allProducts
                    .GroupBy(p => p.CategoryId)
                    .ToDictionary(grupo => grupo.Key, grupo => grupo.Count());

             
                var result = new List<GraphCategoryDTO>();

                foreach (var category in allCategories)
                {
                  
                    countByCategory.TryGetValue(category.Id, out var contagem);

                    result.Add(new GraphCategoryDTO
                    {
                        CategoryName = category.Name, 
                        ProductQuantity = contagem
                    });
                }

                return result;
            }
        }
    
}
