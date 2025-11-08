using AutoMapper;
using ShopAPI.Application.DTOs;
using ShopAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
        
            CreateMap<Product, ProductDTO>();

            CreateMap<Category, CategoryDTO>(); 
        }
    }
}
