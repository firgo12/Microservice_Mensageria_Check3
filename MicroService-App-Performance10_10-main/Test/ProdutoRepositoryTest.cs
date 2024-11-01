using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_app_domain;
using web_app_repository;

namespace Test
{
    public class ProdutoRepositoryTest
    {
        [Fact]
        public async Task ListarProdutos()
        {
            var produtos = new List<Produto>()
            {
                new Produto()
                {
                    ID = 1,
                    Nome = "Bola",
                    Preco = "R$10",
                    Quantidade_Estoque = 10,
                    Data_Criacao = "10/10/2024"
                },
                new Produto()
                {
                    ID = 2,
                    Nome = "Pipa",
                    Preco = "R$20",
                    Quantidade_Estoque = 15,
                    Data_Criacao = "10/10/2024"
                }
            };

            var _productRepositoryMock = new Mock<IProdutoRepository>();
            _productRepositoryMock.Setup(u => u.ListarProdutos()).ReturnsAsync(produtos);
            var productRepository = _productRepositoryMock.Object;

            //Act
            var result = await productRepository.ListarProdutos();

            //Asserts
            Assert.Equal(produtos, result);
        }

        [Fact]
        public async Task SalvarProduto()
        {
            var produto = new Produto()
            {
                ID = 3,
                Nome = "carrinho",
                Preco = "R$5",
                Quantidade_Estoque = 50,
                Data_Criacao = "10/10/2024"
            };
            var productRepositoryMock = new Mock<IProdutoRepository>();
            productRepositoryMock
                .Setup(u => u.SalvarProduto(It.IsAny<Produto>()))
                .Returns(Task.CompletedTask);
            var productRepository = productRepositoryMock.Object;

            //act
            await productRepository.SalvarProduto(produto);

            //asserts
            productRepositoryMock
                .Verify(u => u.SalvarProduto(It.IsAny<Produto>()), Times.Once);
        }
    }
}
