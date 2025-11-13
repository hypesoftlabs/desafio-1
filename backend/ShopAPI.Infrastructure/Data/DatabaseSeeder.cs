// ShopAPI.Infrastructure/Data/DatabaseSeeder.cs
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopAPI.Domain.Entities;

namespace ShopAPI.Infrastructure.Data
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(ShopDbContext context)
        {
           
            if (await context.Categories.AnyAsync())
                return;

        
            var categories = new List<Category>
            {
                new Category("Informática"),
                new Category("Eletrônicos"),
                new Category("Eletrodomésticos"),
                new Category("Escritório"),
                new Category("Vestuário"),
                new Category("Calçados"),
                new Category("Higiene"),
                new Category("Alimentos"),
                new Category("Bebidas"),
                new Category("Brinquedos"),
            };

            await context.Categories.AddRangeAsync(categories);
            await context.SaveChangesAsync();

            var catByName = categories.ToDictionary(c => c.Name, c => c.Id);

          
            var products = new List<Product>
            {
                new Product(
                    "Notebook Dell 15\"",
                    "Notebook para uso diário com 8GB RAM",
                    3500,
                    catByName["Informática"],
                    5
                ),
                new Product(
                    "Mouse Sem Fio",
                    "Mouse óptico sem fio 2.4G",
                    80,
                    catByName["Informática"],
                    20
                ),
                new Product(
                    "Smart TV 50\"",
                    "Smart TV 4K UHD",
                    2800,
                    catByName["Eletrônicos"],
                    3
                ),
                new Product(
                    "Fone Bluetooth",
                    "Fone de ouvido bluetooth com case",
                    150,
                    catByName["Eletrônicos"],
                    15
                ),
                new Product(
                    "Geladeira Frost Free",
                    "Geladeira 400L Frost Free",
                    3800,
                    catByName["Eletrodomésticos"],
                    2
                ),
                new Product(
                    "Micro-ondas 30L",
                    "Micro-ondas espelhado 30L",
                    700,
                    catByName["Eletrodomésticos"],
                    4
                ),
                new Product(
                    "Cadeira de Escritório",
                    "Cadeira ergonômica com apoio de lombar",
                    650,
                    catByName["Escritório"],
                    6
                ),
                new Product(
                    "Camisa Social Masculina",
                    "Camisa social algodão tamanho M",
                    120,
                    catByName["Vestuário"],
                    12
                ),
                new Product(
                    "Tênis Esportivo",
                    "Tênis para corrida tamanho 40",
                    220,
                    catByName["Calçados"],
                    10
                ),
                new Product(
                    "Shampoo 400ml",
                    "Shampoo hidratante para cabelos secos",
                    25,
                    catByName["Higiene"],
                    30
                ),
                new Product(
                    "Arroz 5kg",
                    "Arroz branco tipo 1",
                    30,
                    catByName["Alimentos"],
                    25
                ),
                new Product(
                    "Refrigerante 2L",
                    "Refrigerante sabor cola",
                    9.5,
                    catByName["Bebidas"],
                    40
                ),
                new Product(
                    "Carrinho de Controle Remoto",
                    "Carrinho brinquedo com controle",
                    150,
                    catByName["Brinquedos"],
                    8
                ),
            };

            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
    }
}
