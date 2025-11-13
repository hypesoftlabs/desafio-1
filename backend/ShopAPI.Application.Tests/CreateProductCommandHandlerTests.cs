using Moq;
using ShopAPI.Application.Commands;
using ShopAPI.Application.Handlers;
using ShopAPI.Domain.Entities;
using ShopAPI.Domain.Repositories;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Tests
{
    public class CreateProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly CreateProductHandler _handler;

    
        public CreateProductCommandHandlerTests()
        {
            _mockRepo = new Mock<IProductRepository>();
            _handler = new CreateProductHandler(_mockRepo.Object);
        }

        [Fact] // Marca como um teste
        public async Task Handle_DeveChamarAddAsyncNoRepositorio_QuandoExecutado()
        {
       
            var command = new CreateProductCommand
            {
                Name = "Produto Teste",
                Description = "Descrição Teste",
                Price = 100,
                CategoryId = "categoria-id-falsa",
                Quantity = 10
            };


            var resultadoId = await _handler.Handle(command, CancellationToken.None);

            // --- ASSERT (Verificar) ---

            resultadoId.Should().NotBeNullOrEmpty();

            _mockRepo.Verify(
                r => r.AddAsync(It.IsAny<Product>()),
                Times.Once
            );
        }
    }
}
