using FluentAssertions;
using ShopAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Tests
{
    public class CategoryTests
    {

        [Fact]
        public void Constructor_ShouldThrowArgumentException_WhenNameIsEmpty()
        {

            Action act = () => new Category("");

            act.Should().Throw<ArgumentException>()
               .WithMessage("O nome da categoria não pode ser vazio.");
        }

        [Fact]
        public void UpdateName_ShouldThrowArgumentException_WhenNewNameIsEmpty()
        {
            var category = new Category("nome");
            Action act = () => category.UpdateName("");
            act.Should().Throw<ArgumentException>()
               .WithMessage("O nome da categoria não pode ser vazio.");
        }


    }
}
