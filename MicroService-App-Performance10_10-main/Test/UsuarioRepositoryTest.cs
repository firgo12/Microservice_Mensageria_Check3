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
    public class UsuarioRepositoryTest
    {
        [Fact]
        public async Task ListarUsuarios()
        {
            var usuarios = new List<Usuario>()
            {
                new Usuario()
                {
                    Email = "xxx@gmail.com",
                    ID = 1,
                    Nome = "Guilherme Mendes da Cunha"
                },
                new Usuario()
                {
                    Email = "www@gmail.com",
                    ID = 2,
                    Nome = "Gustavo"
                }
            };

            var _userRepositoryMock = new Mock<IUsuarioRepository>();
            _userRepositoryMock.Setup(u => u.ListarUsuarios()).ReturnsAsync(usuarios);
            var userRepository = _userRepositoryMock.Object;

            //Act
            var result = await userRepository.ListarUsuarios();

            //Asserts
            Assert.Equal(usuarios, result);


        }

        [Fact]
        public async Task SalvarUsuario()
        {
            var usuario = new Usuario()
            {
                ID = 2,
                Email = "xxx@fiap.com.br",
                Nome = "Guilherme Mendes da Cunha"
            };
            var userRepositoryMock = new Mock<IUsuarioRepository>();
            userRepositoryMock
                .Setup(u => u.SalvarUsuario(It.IsAny<Usuario>()))
                .Returns(Task.CompletedTask);
            var userRepository = userRepositoryMock.Object;

            //act
            await userRepository.SalvarUsuario(usuario);

            //asserts
            userRepositoryMock
                .Verify(u => u.SalvarUsuario(It.IsAny<Usuario>()), Times.Once);
        }
    }
}
