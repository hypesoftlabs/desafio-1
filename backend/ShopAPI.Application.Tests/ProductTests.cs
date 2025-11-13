using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using ShopAPI.Domain.Entities; 


namespace ShopAPI.Application.Tests
{
    public class ProductTests
    {
      
        [Fact]
        public void Constructor_ShouldThrowArgumentException_WhenNameIsEmpty()
        {
         
            Action act = () => new Product(
                "", 
                "Descrição válida",
                100,
                "category-id",
                10
            );

      
            act.Should().Throw<ArgumentException>()
               .WithMessage("O nome não pode ser vazio."); 
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentException_WhenPriceIsNegative()
        {
          
            Action act = () => new Product(
                "Nome Válido",
                "Descrição válida",
                -10, 
                "category-id",
                10
            );

          
            act.Should().Throw<ArgumentException>()
               .WithMessage("O preço deve ser maior que 0.");
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentException_WhenCategoryIdIsEmpty()
        {
          
            Action act = () => new Product(
                "Nome Válido",
                "Descrição válida",
                100,
                "", 
                10
            );

           
            act.Should().Throw<ArgumentException>()
               .WithMessage("O ID da categoria é obrigatório.");
        }



        [Fact]
        public void ChangeName_ShouldThrowArgumentException_WhenNewNameIsEmpty()
        {
            
            var product = new Product("Nome Antigo", "Desc", 100, "cat1", 10);
            Action act = () => product.ChangeName(""); 
            act.Should().Throw<ArgumentException>()
               .WithMessage("O nome não pode ser nulo ou vazio");
        }

        [Fact]
        public void ChangeQuantity_ShouldThrowArgumentException_WhenNewQuantityIsNegative()
        {
          
            var product = new Product("Nome", "Desc", 100, "cat1", 10);
            Action act = () => product.ChangeQuantity(-5); // Tenta mudar para inválido

            act.Should().Throw<ArgumentException>()
               .WithMessage("O estoque não pode ser negativo.");
        }

        [Fact]
        public void ChangePrice_ShouldThrowArgumentException_WhenNewPriceIsNegative()
        {
  
            var product = new Product("Nome", "Desc", 100, "cat1", 10);
            Action act = () => product.ChangePrice(-20); // Tenta mudar para inválido

            act.Should().Throw<ArgumentException>()
               .WithMessage("O preço não pode ser negativo.");
        }

        [Fact]
        public void ChangeDescription_ShouldThrowArgumentException_WhenNewDescriptionIsEmpty()
        {

            var product = new Product("Nome Antigo", "Desc", 100, "cat1", 10);
            Action act = () => product.ChangeDescription("");
            act.Should().Throw<ArgumentException>()
               .WithMessage("A descrição não pode ser nula ou vazia");
        }

        [Fact]
        public void ChangeCategory_ShouldThrowArgumentException_WhenNewCategoryIdIsEmpty()
        {

            var product = new Product("Nome Antigo", "Desc", 100, "cat1", 10);
            Action act = () => product.ChangeCategory("");
            act.Should().Throw<ArgumentException>()
               .WithMessage("O ID da categoria é obrigatório.");
        }


    }

}
