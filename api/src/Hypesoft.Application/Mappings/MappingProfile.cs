using AutoMapper;
using Hypesoft.Application.Dtos;
using Hypesoft.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypesoft.Application.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ReverseMap();


            CreateMap<Category, CategoryDto>();

            CreateMap<ProductDto, Product>();
            CreateMap<CategoryDto, Category>();
        }
    }
}
