using NSubstitute;
using Domain.Requests;
using Domain.Interfaces.Repositories;
using Domain.Entities;
using ControladorEstoqueWebAPI.Services;
using FluentAssertions;

namespace ControladorEstoqueTests
{
    public class ProdutoServiceTests
    {
        [Fact]
        public void AdicionarProduto_DeveCriarNovoProduto_QuandoNomeEhValido()
        {
            // Arrange
            var request = new ProdutoRequest() { Nome = "Teste" };
            var produtoRepositorySubstitute = Substitute.For<IProdutoRepository>();
            var produtoCriadoMock = new Produto()
            {
                Id = 0,
                Nome = request.Nome,
                Lotes = new()
            };

            produtoRepositorySubstitute.Create(Arg.Any<Produto>()).Returns(produtoCriadoMock);

            var produtoService = new ProdutoService(produtoRepositorySubstitute);

            // Act
            var produto = produtoService.AdicionarProduto(request);

            // Assert

            produto.Should().NotBeNull();
            produto.Nome.Should().Be(request.Nome);
            produto.Lotes.Should().NotBeNull();
            produto.Id.Should().Be(produtoCriadoMock.Id);
        }

        [Fact]
        public void AtualizarProduto_DeveLancarErro_QuandoIdDeProdutoEhInvalido()
        {
            // Arrange
            var request = new ProdutoRequest() { Nome = "Teste" };
            var produtoRepositorySubstitute = Substitute.For<IProdutoRepository>();

            produtoRepositorySubstitute.GetByID(Arg.Any<int>()).Returns((Produto)null);
            produtoRepositorySubstitute.Update(Arg.Any<Produto>());

            var produtoService = new ProdutoService(produtoRepositorySubstitute);

            // Act
            Action atualizarProduto_Action = () => produtoService.AtualizarProduto(request, 1);

            // Assert
            atualizarProduto_Action.Should().Throw<ArgumentException>().WithMessage("Não existe um produto com ID 1.");
        }

        [Fact]
        public void AtualizarProduto_DeveAtualizarProduto_QuandoIDdeProdutoForValido()
        {
            // Arrange
            var request = new ProdutoRequest() { Nome = "Teste" };
            var produtoRepositorySubstitute = Substitute.For<IProdutoRepository>();

            var produtoAntigo = new Produto()
            {
                Id = 0,
                Nome = "Nome antigo",
                Lotes = new()
            };

            produtoRepositorySubstitute.GetByID(Arg.Any<int>()).Returns(produtoAntigo);
            produtoRepositorySubstitute.Update(Arg.Any<Produto>());

            var produtoService = new ProdutoService(produtoRepositorySubstitute);

            // Act
            var produtoAtualizado = produtoService.AtualizarProduto(request, 0);

            // Assert
            produtoAtualizado.Should().NotBeNull();
            produtoAtualizado.Nome.Should().Be(request.Nome);
            produtoAtualizado.Lotes.Should().NotBeNull();
            produtoAtualizado.Id.Should().Be(0);
        }

        [Fact]
        public void ListarProdutoPorID_DeveLancarErro_QuandoNaoExistirProdutoComEsteID()
        {
            // Arrange
            var request = new ProdutoRequest() { Nome = "Teste" };
            var produtoRepositorySubstitute = Substitute.For<IProdutoRepository>();

            produtoRepositorySubstitute.GetByID(Arg.Any<int>()).Returns((Produto)null);

            var produtoService = new ProdutoService(produtoRepositorySubstitute);

            // Act
            Action listarProduto_action = () => produtoService.ListarProdutoPorID(1);

            // Assert
            listarProduto_action.Should().Throw<ArgumentException>().WithMessage("Não existe um produto com ID 1.");
        }

        [Fact]
        public void ListarProdutoPorID_DeveRetornarProduto_QuandoExistirProdutoComEsteID()
        {
            // Arrange
            var produto = new Produto()
            {
                Id = 1,
                Nome = "Nome antigo",
                Lotes = new()
            };

            var produtoRepositorySubstitute = Substitute.For<IProdutoRepository>();

            produtoRepositorySubstitute.GetByID(Arg.Any<int>()).Returns(produto);

            var produtoService = new ProdutoService(produtoRepositorySubstitute);

            // Act
            var produtoLido = produtoService.ListarProdutoPorID(1);

            // Assert
            produtoLido.Should().NotBeNull();
            produtoLido.Nome.Should().Be(produto.Nome);
            produtoLido.Id.Should().Be(produto.Id);
        }

        [Fact]
        public void ListarProdutoPorNome_DeveLancarErro_QuandoNaoExistirProdutosComEsteNome()
        {
            // Arrange
            var produtoRepositorySubstitute = Substitute.For<IProdutoRepository>();

            produtoRepositorySubstitute.GetByName(Arg.Any<string>()).Returns(Enumerable.Empty<Produto>());

            var produtoService = new ProdutoService(produtoRepositorySubstitute);

            // Act
            Action listarProdutoPorNome_action = () => produtoService.ListarProdutosPorNome("Inexistente");

            // Assert
            listarProdutoPorNome_action.Should().Throw<ArgumentException>().WithMessage($"Não existem produtos de nome Inexistente.");
        }

        [Fact]
        public void ListarProdutoPorNome_DeveRetornarProdutos_QuandoExistirProdutosComEsteNome()
        {
            // Arrange
            var produtos = new List<Produto>()
        {
            new Produto { Id = 1, Nome = "Camisa Polo Branca" },
            new Produto { Id = 2, Nome = "Camisa Polo Preta" }
        };

            var produtoRepositorySubstitute = Substitute.For<IProdutoRepository>();

            produtoRepositorySubstitute.GetByName(Arg.Any<string>()).Returns(produtos);

            var produtoService = new ProdutoService(produtoRepositorySubstitute);

            // Act
            var produtosLidos = produtoService.ListarProdutosPorNome("Polo");

            // Assert
            produtosLidos.Should().NotBeNull();
            produtosLidos.Should().HaveCount(2);
            produtosLidos.Should().ContainEquivalentOf(produtos.ElementAt(0));
            produtosLidos.Should().ContainEquivalentOf(produtos.ElementAt(1));
        }

        [Fact]
        public void ListarTodosOsProdutos_DeveLancarErro_QuandoNaoExistirProdutosRegistrados()
        {
            // Arrange
            var produtoRepositorySubstitute = Substitute.For<IProdutoRepository>();

            produtoRepositorySubstitute.GetAll().Returns(Enumerable.Empty<Produto>());

            var produtoService = new ProdutoService(produtoRepositorySubstitute);

            // Act
            Action listarTodosOsProdutos_action = () => produtoService.ListarTodosOsProdutos();

            // Assert
            listarTodosOsProdutos_action.Should().Throw<ArgumentException>().WithMessage("Não há produtos registrados.");
        }

        [Fact]
        public void ListarTodosOsProdutos_DeveRetornarProdutos_QuandoExistirProdutosRegistrados()
        {
            // Arrange
            var produtos = new List<Produto>()
        {
            new Produto { Id = 1, Nome = "Camisa Polo Branca" },
            new Produto { Id = 2, Nome = "Camisa Polo Preta" }
        };
            var produtoRepositorySubstitute = Substitute.For<IProdutoRepository>();

            produtoRepositorySubstitute.GetAll().Returns(produtos);

            var produtoService = new ProdutoService(produtoRepositorySubstitute);

            // Act
            var produtosLidos = produtoService.ListarTodosOsProdutos();

            // Assert
            produtosLidos.Should().NotBeNull();
            produtosLidos.Should().HaveCount(2);
            produtosLidos.Should().ContainEquivalentOf(produtos.ElementAt(0));
            produtosLidos.Should().ContainEquivalentOf(produtos.ElementAt(1));
        }

        [Fact]
        public void RemoverProduto_DeveLancarErro_QuandoNaoExistirProdutoComEsteID()
        {
            // Arrange
            int idProduto = 2;

            var produtoRepositorySubstitute = Substitute.For<IProdutoRepository>();

            produtoRepositorySubstitute.GetByID(Arg.Any<int>()).Returns((Produto)null);
            produtoRepositorySubstitute.Delete(Arg.Any<int>()).Returns((Produto)null);

            var produtoService = new ProdutoService(produtoRepositorySubstitute);

            // Act
            Action removerProduto_action = () => produtoService.RemoverProduto(idProduto);

            // Assert
            removerProduto_action.Should().Throw<ArgumentException>().WithMessage($"Não existe um produto com ID {idProduto}.");
        }

        [Fact]
        public void RemoverProduto_DeveRetornarProduto_QuandoExistirProdutoComEsteID()
        {
            // Arrange
            var produtoRemovidoMock = new Produto()
            {
                Id = 1,
                Nome = "Produto removido",
                Lotes = new List<Lote>()
            };

            var produtoRepositorySubstitute = Substitute.For<IProdutoRepository>();

            produtoRepositorySubstitute.GetByID(Arg.Any<int>()).Returns(produtoRemovidoMock);
            produtoRepositorySubstitute.Delete(Arg.Any<int>()).Returns(produtoRemovidoMock);

            var produtoService = new ProdutoService(produtoRepositorySubstitute);

            // Act
            var produtoRemovido = produtoService.RemoverProduto(produtoRemovidoMock.Id);

            // Assert
            produtoRemovido.Id.Should().Be(1);
            produtoRemovido.Should().NotBeNull();
            produtoRemovido.Nome.Should().Be(produtoRemovidoMock.Nome);
        }
    }
}