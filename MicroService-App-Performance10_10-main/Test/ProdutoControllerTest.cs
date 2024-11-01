using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_app_domain;
using web_app_performance.Controllers;
using web_app_repository;

namespace Test
{
    public class ProdutoControllerTest
    {
        private readonly Mock<IProdutoRepository> _productRepositoryMock;
        private readonly ProdutoController _controller;

        public ProdutoControllerTest()
        {
            _productRepositoryMock = new Mock<IProdutoRepository>();
            _controller = new ProdutoController( _productRepositoryMock.Object );
        }
        [Fact]
        public async Task Get_ListarProdutosOk()
        {
            //arrange
            var produtos = new List<Produto>()
            {
                new Produto()
                {
                    ID = 1,
                    Nome = "Bola",
                    Preco = "R$10",
                    Quantidade_Estoque = 10,
                    Data_Criacao = "10/10/2024"
                }
            };
            _productRepositoryMock.Setup(r => r.ListarProdutos()).ReturnsAsync(produtos);


            //Act
            var result = await _controller.GetProduto();

            //Asserts
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(JsonConvert.SerializeObject(produtos), JsonConvert.SerializeObject(okResult.Value));

        }

        [Fact]
        public async Task Get_ListarRetornaNotFound()
        {
            _productRepositoryMock.Setup(u => u.ListarProdutos())
                .ReturnsAsync((IEnumerable<Produto>)null);

            var result = await _controller.GetProduto();

            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async Task Post_SalvarProduto()
        {
            var produto = new Produto()
            {
                ID = 2,
                Nome = "Pipa",
                Preco = "R$20",
                Quantidade_Estoque = 15,
                Data_Criacao = "10/10/2024"
            };
            _productRepositoryMock.Setup(u => u.SalvarProduto(It.IsAny<Produto>()))
                .Returns(Task.CompletedTask);

            //Act
            var result = await _controller.Post(produto);

            //Asserts
            _productRepositoryMock
                .Verify(u => u.SalvarProduto(It.IsAny<Produto>()), Times.Once);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
