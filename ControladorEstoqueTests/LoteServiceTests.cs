using NSubstitute;
using Domain.Requests;
using Domain.Interfaces.Repositories;
using Domain.Entities;
using ControladorEstoqueWebAPI.Services;
using FluentAssertions;
using Domain.Interfaces.Services;

namespace ControladorEstoqueTests
{
    public class LoteServiceTests
    {
        [Fact]
        public void AdicionarLote_DeveCriarNovoLote_QuandoDadosSaoValidos()
        {
            // Arrange
            var request = new LoteRequest()
            {
                DataValidade = "11/03/2080",
                UnidadesProdutos = 800,
                ProdutoId = 1
            };

            var produtoRepositorySubstitute = Substitute.For<IProdutoRepository>();
            var loteRepositorySubstitute = Substitute.For<ILoteRepository>();

            var produtoCriadoMock = new Produto()
            {
                Id = 1,
                Nome = "Produto 1",
                Lotes = new()
            };

            produtoRepositorySubstitute.GetByID(Arg.Any<int>()).Returns(produtoCriadoMock);
            loteRepositorySubstitute.Create(Arg.Any<Lote>()).Returns(1);

            var loteService = new LoteService(loteRepositorySubstitute, produtoRepositorySubstitute);

            // Act
            var lote = loteService.AdicionarLote(request);

            // Assert
            lote.Should().NotBeNull();
            lote.Id.Should().Be(1);
            lote.ProdutoId.Should().Be(produtoCriadoMock.Id);
            lote.Validade.Should().Be(DateOnly.Parse("11/03/2080"));
        }

        [Fact]
        public void AdicionarLote_DeveLancarExcecao_QuandoDataDeValidadeEhInvalida()
        {
            // Arrange
            var request = new LoteRequest()
            {
                DataValidade = "11232080",
                UnidadesProdutos = 800,
                ProdutoId = 1
            };

            var produtoRepositorySubstitute = Substitute.For<IProdutoRepository>();
            var loteRepositorySubstitute = Substitute.For<ILoteRepository>();

            var produtoCriadoMock = new Produto()
            {
                Id = 0,
                Nome = "Produto 1",
                Lotes = new()
            };

            produtoRepositorySubstitute.GetByID(Arg.Any<int>()).Returns(produtoCriadoMock);
            loteRepositorySubstitute.Create(Arg.Any<Lote>()).Returns(0);

            var loteService = new LoteService(loteRepositorySubstitute, produtoRepositorySubstitute);

            // Act / Assert
            Action adicionarLoteAction = () => loteService.AdicionarLote(request);
            adicionarLoteAction.Should().Throw<ArgumentException>().WithMessage("Data de validade inserida não está em um formato válido.");
        }

        [Fact]
        public void AdicionarLote_DeveLancarExcecao_QuandoIdProdutoEhInvalido()
        {
            // Arrange
            var request = new LoteRequest()
            {
                DataValidade = "11232080",
                UnidadesProdutos = 800,
                ProdutoId = 1
            };

            var produtoRepositorySubstitute = Substitute.For<IProdutoRepository>();
            var loteRepositorySubstitute = Substitute.For<ILoteRepository>();

            produtoRepositorySubstitute.GetByID(Arg.Any<int>()).Returns((Produto)null);
            loteRepositorySubstitute.Create(Arg.Any<Lote>()).Returns(0);

            var loteService = new LoteService(loteRepositorySubstitute, produtoRepositorySubstitute);

            // Act
            Action adicionarLoteAction = () => loteService.AdicionarLote(request);

            // Assert
            adicionarLoteAction.Should().Throw<ArgumentException>().WithMessage($"Não existe um produto de ID {request.ProdutoId}.");
        }

        [Fact]
        public void AtualizarLote_DeveAtualizarLote_QuandoDadosSaoValidos()
        {
            // Arrange
            var loteAntigo = new Lote()
            {
                Id = 0,
                Validade = DateOnly.Parse("11/03/2080"),
                UnidadesProdutos = 800,
                ProdutoId = 1
            };

            var request = new LoteRequest()
            {
                DataValidade = "11/03/2080",
                UnidadesProdutos = 1000,
                ProdutoId = 1
            };

            var produtoRepositorySubstitute = Substitute.For<IProdutoRepository>();
            var loteRepositorySubstitute = Substitute.For<ILoteRepository>();

            var produtoMock = new Produto()
            {
                Id = 0,
                Nome = "Produto 1",
                Lotes = new()
            };

            produtoRepositorySubstitute.GetByID(Arg.Any<int>()).Returns(produtoMock);
            loteRepositorySubstitute.GetByID(Arg.Any<int>()).Returns(loteAntigo);
            loteRepositorySubstitute.Update(Arg.Any<Lote>());

            var loteService = new LoteService(loteRepositorySubstitute, produtoRepositorySubstitute);

            // Act
            var lote = loteService.AtualizarLote(request, 0);

            // Assert
            lote.Should().NotBeNull();
            lote.Id.Should().Be(0);
            lote.ProdutoId.Should().Be(produtoMock.Id);
            lote.Validade.Should().Be(DateOnly.Parse("11/03/2080"));
        }

        [Fact]
        public void AtualizarLote_DeveLancarExcecao_QuandoNaoExisteLoteComIdEspecificado()
        {
            // Arrange
            var loteAntigo = new Lote()
            {
                Id = 0,
                Validade = DateOnly.Parse("11/03/2080"),
                UnidadesProdutos = 800,
                ProdutoId = 1
            };

            var request = new LoteRequest()
            {
                DataValidade = "11/03/2080",
                UnidadesProdutos = 1000,
                ProdutoId = 1
            };

            var produtoRepositorySubstitute = Substitute.For<IProdutoRepository>();
            var loteRepositorySubstitute = Substitute.For<ILoteRepository>();

            var produtoMock = new Produto()
            {
                Id = 1,
                Nome = "Produto 1",
                Lotes = new()
            };

            produtoRepositorySubstitute.GetByID(Arg.Any<int>()).Returns(produtoMock);
            loteRepositorySubstitute.GetByID(Arg.Any<int>()).Returns((Lote)null);
            loteRepositorySubstitute.Update(Arg.Any<Lote>());

            var loteService = new LoteService(loteRepositorySubstitute, produtoRepositorySubstitute);

            // Act / Assert
            Action adicionarLoteAction = () => loteService.AtualizarLote(request, 1);
            adicionarLoteAction.Should().Throw<ArgumentException>().WithMessage($"Não existe um lote de ID 1.");
        }

        [Fact]
        public void AtualizarLote_DeveLancarExcecao_QuandoNaoExisteProdutoComIdEspecificado()
        {
            // Arrange
            var loteAntigo = new Lote()
            {
                Id = 1,
                Validade = DateOnly.Parse("11/03/2080"),
                UnidadesProdutos = 800,
                ProdutoId = 1
            };

            var request = new LoteRequest()
            {
                DataValidade = "11/03/2080",
                UnidadesProdutos = 1000,
                ProdutoId = 1
            };

            var produtoRepositorySubstitute = Substitute.For<IProdutoRepository>();
            var loteRepositorySubstitute = Substitute.For<ILoteRepository>();

            produtoRepositorySubstitute.GetByID(Arg.Any<int>()).Returns((Produto)null);
            loteRepositorySubstitute.GetByID(Arg.Any<int>()).Returns(loteAntigo);
            loteRepositorySubstitute.Update(Arg.Any<Lote>());

            var loteService = new LoteService(loteRepositorySubstitute, produtoRepositorySubstitute);

            // Act / Assert
            Action adicionarLoteAction = () => loteService.AtualizarLote(request, 1);
            adicionarLoteAction.Should().Throw<ArgumentException>().WithMessage($"Não existe um produto de ID {request.ProdutoId}.");
        }

        [Fact]
        public void ListarLotePorCodigo_DeveRetornarLote_QuandoCodigoForValido()
        {
            // Arrange
            var lote = new Lote()
            {
                Id = 0,
                Validade = DateOnly.Parse("11/03/2080"),
                UnidadesProdutos = 800,
                ProdutoId = 1
            };

            var produtoRepositorySubstitute = Substitute.For<IProdutoRepository>();
            var loteRepositorySubstitute = Substitute.For<ILoteRepository>();

            loteRepositorySubstitute.GetByCode(Arg.Any<string>()).Returns(lote);

            var loteService = new LoteService(loteRepositorySubstitute, produtoRepositorySubstitute);

            // Act
            var loteEncontrado = loteService.ListarLotePorCodigo("CodigoValido");

            // Assert
            loteEncontrado.Should().NotBeNull();
            loteEncontrado.Id.Should().Be(0);
            loteEncontrado.ProdutoId.Should().Be(1);
            loteEncontrado.Validade.Should().Be(DateOnly.Parse("11/03/2080"));
        }

        [Fact]
        public void ListarLotePorCodigo_DeveLancarExcecao_QuandoNaoExistirLoteComEsteCodigo()
        {
            // Arrange
            var lote = new Lote()
            {
                Id = 0,
                Validade = DateOnly.Parse("11/03/2080"),
                UnidadesProdutos = 800,
                ProdutoId = 1
            };

            var produtoRepositorySubstitute = Substitute.For<IProdutoRepository>();
            var loteRepositorySubstitute = Substitute.For<ILoteRepository>();

            loteRepositorySubstitute.GetByCode(Arg.Any<string>()).Returns((Lote)null);

            var loteService = new LoteService(loteRepositorySubstitute, produtoRepositorySubstitute);

            // Act / Assert
            Action listarLotePorCodigoAction = () => loteService.ListarLotePorCodigo("CodigoInvalido");
            listarLotePorCodigoAction.Should().Throw<ArgumentException>().WithMessage("Não existe um lote de código CodigoInvalido.");
        }

        [Fact]
        public void ListarLotePorID_DeveRetornarLote_QuandoIdForValido()
        {
            // Arrange
            var lote = new Lote()
            {
                Id = 0,
                Validade = DateOnly.Parse("11/03/2080"),
                UnidadesProdutos = 800,
                ProdutoId = 1
            };

            var produtoRepositorySubstitute = Substitute.For<IProdutoRepository>();
            var loteRepositorySubstitute = Substitute.For<ILoteRepository>();

            loteRepositorySubstitute.GetByID(Arg.Any<int>()).Returns(lote);

            var loteService = new LoteService(loteRepositorySubstitute, produtoRepositorySubstitute);

            // Act
            var loteEncontrado = loteService.ListarLotePorID(0);

            // Assert
            loteEncontrado.Should().NotBeNull();
            loteEncontrado.Id.Should().Be(0);
            loteEncontrado.ProdutoId.Should().Be(1);
            loteEncontrado.Validade.Should().Be(DateOnly.Parse("11/03/2080"));
        }

        [Fact]
        public void ListarLotePorID_DeveLancarExcecao_QuandoIdForInvalido()
        {
            // Arrange
            var lote = new Lote()
            {
                Id = 0,
                Validade = DateOnly.Parse("11/03/2080"),
                UnidadesProdutos = 800,
                ProdutoId = 1
            };

            var produtoRepositorySubstitute = Substitute.For<IProdutoRepository>();
            var loteRepositorySubstitute = Substitute.For<ILoteRepository>();

            loteRepositorySubstitute.GetByID(Arg.Any<int>()).Returns((Lote)null);

            var loteService = new LoteService(loteRepositorySubstitute, produtoRepositorySubstitute);

            // Act / Assert
            Action listarLotePorIdAction = () => loteService.ListarLotePorID(1);
            listarLotePorIdAction.Should().Throw<ArgumentException>().WithMessage($"Não existe um lote de ID 1.");
        }

        [Fact]
        public void ListarLotesPorProduto_DeveRetornarLotes_QuandoIdDeProdutoForValidoEExistirLotes()
        {
            // Arrange
            var produto = new Produto()
            {
                Id = 0,
                Nome = "Produto 1",
                Lotes = new()
            };

            var lote = new Lote()
            {
                Id = 0,
                Validade = DateOnly.Parse("11/03/2080"),
                UnidadesProdutos = 800,
                ProdutoId = 1
            };

            var produtoRepositorySubstitute = Substitute.For<IProdutoRepository>();
            var loteRepositorySubstitute = Substitute.For<ILoteRepository>();

            loteRepositorySubstitute.GetByID(Arg.Any<int>()).Returns(lote);
            produtoRepositorySubstitute.GetByID(Arg.Any<int>()).Returns(produto);
            loteRepositorySubstitute.GetAllByProduct(Arg.Any<int>()).Returns([lote]);

            var loteService = new LoteService(loteRepositorySubstitute, produtoRepositorySubstitute);

            // Act
            var lotesEncontrados = loteService.ListarLotesPorProduto(0);

            // Assert
            lotesEncontrados.Should().NotBeNull();
            lotesEncontrados.Should().HaveCount(1);
            lotesEncontrados.ElementAt(0).Id.Should().Be(0);
        }

        [Fact]
        public void ListarLotesPorProduto_DeveLancarExcecao_QuandoIdDeProdutoForValidoMasNaoExistirLotes()
        {
            // Arrange
            var produto = new Produto()
            {
                Id = 0,
                Nome = "Produto 1",
                Lotes = new()
            };

            var produtoRepositorySubstitute = Substitute.For<IProdutoRepository>();
            var loteRepositorySubstitute = Substitute.For<ILoteRepository>();

            produtoRepositorySubstitute.GetByID(Arg.Any<int>()).Returns(produto);
            loteRepositorySubstitute.GetAllByProduct(Arg.Any<int>()).Returns([]);

            var loteService = new LoteService(loteRepositorySubstitute, produtoRepositorySubstitute);

            // Act / Assert
            var listarLotesPorProdutoAction = () => loteService.ListarLotesPorProduto(0);
            listarLotesPorProdutoAction.Should().Throw<ArgumentException>().WithMessage("Não existem lotes de produtos com este ID.");
        }

        [Fact]
        public void ListarLotesPorProduto_DeveLancarExcecao_QuandoIdDeProdutoForInvalido()
        {
            // Arrange
            var produto = new Produto()
            {
                Id = 0,
                Nome = "Produto 1",
                Lotes = new()
            };

            var produtoRepositorySubstitute = Substitute.For<IProdutoRepository>();
            var loteRepositorySubstitute = Substitute.For<ILoteRepository>();

            produtoRepositorySubstitute.GetByID(Arg.Any<int>()).Returns((Produto)null);
            loteRepositorySubstitute.GetAllByProduct(Arg.Any<int>()).Returns([]);

            var loteService = new LoteService(loteRepositorySubstitute, produtoRepositorySubstitute);

            // Act / Assert
            var listarLotesPorProdutoAction = () => loteService.ListarLotesPorProduto(0);
            listarLotesPorProdutoAction.Should().Throw<ArgumentException>().WithMessage($"Não existe um produto de ID {produto.Id}.");
        }

        [Fact]
        public void ListarLotesVencidos_DeveRetornarLotes_QuandoExistirAlgumLoteVencido()
        {
            // Arrange
            var loteVencido = new Lote()
            {
                Id = 0,
                Validade = DateOnly.Parse("11/03/2020"),
                UnidadesProdutos = 800,
                ProdutoId = 1
            };

            var loteNaoVencido = new Lote()
            {
                Id = 1,
                Validade = DateOnly.Parse("11/03/2080"),
                UnidadesProdutos = 800,
                ProdutoId = 1
            };

            var produtoRepositorySubstitute = Substitute.For<IProdutoRepository>();
            var loteRepositorySubstitute = Substitute.For<ILoteRepository>();

            loteRepositorySubstitute.GetAll().Returns([loteVencido, loteNaoVencido]);

            var loteService = new LoteService(loteRepositorySubstitute, produtoRepositorySubstitute);

            // Act
            var lotesVencidos = loteService.ListarLotesVencidos();

            // Assert
            lotesVencidos.Should().NotBeNull();
            lotesVencidos.Should().HaveCount(1);
            lotesVencidos.ElementAt(0).Id.Should().Be(0);
        }

        [Fact]
        public void ListarLotesVencidos_DeveLancarExcecao_QuandoNaoExistirNenhumLoteVencido()
        {
            // Arrange
            var loteNaoVencido1 = new Lote()
            {
                Id = 0,
                Validade = DateOnly.Parse("11/03/2070"),
                UnidadesProdutos = 800,
                ProdutoId = 1
            };

            var loteNaoVencido2 = new Lote()
            {
                Id = 1,
                Validade = DateOnly.Parse("11/03/2080"),
                UnidadesProdutos = 800,
                ProdutoId = 1
            };

            var produtoRepositorySubstitute = Substitute.For<IProdutoRepository>();
            var loteRepositorySubstitute = Substitute.For<ILoteRepository>();

            loteRepositorySubstitute.GetAll().Returns([loteNaoVencido1, loteNaoVencido2]);

            var loteService = new LoteService(loteRepositorySubstitute, produtoRepositorySubstitute);

            // Act / Assert
            Action lotesVencidosAction = () => loteService.ListarLotesVencidos();

            lotesVencidosAction.Should().Throw<ArgumentException>().WithMessage("Não há lotes vencidos em registro.");
        }

        [Fact]
        public void ListarTodosOsLotes_DeveRetornarLotes_QuandoHouverAlgumLoteRegistrado()
        {
            // Arrange
            var lote1 = new Lote()
            {
                Id = 0,
                Validade = DateOnly.Parse("11/03/2036"),
                UnidadesProdutos = 800,
                ProdutoId = 1
            };

            var lote2 = new Lote()
            {
                Id = 1,
                Validade = DateOnly.Parse("11/03/2045"),
                UnidadesProdutos = 7640,
                ProdutoId = 2
            };

            var produtoRepositorySubstitute = Substitute.For<IProdutoRepository>();
            var loteRepositorySubstitute = Substitute.For<ILoteRepository>();

            loteRepositorySubstitute.GetAll().Returns([lote1, lote2]);

            var loteService = new LoteService(loteRepositorySubstitute, produtoRepositorySubstitute);

            // Act
            var lotes = loteService.ListarTodosOsLotes();

            // Assert
            lotes.Should().NotBeNull();
            lotes.Should().HaveCount(2);
            lotes.ElementAt(0).Id.Should().Be(0);
            lotes.ElementAt(1).Id.Should().Be(1);
        }

        [Fact]
        public void ListarTodosOsLotes_DeveLancarExcecao_QuandoNaoHouverNenhumLoteRegistrado()
        {
            // Arrange
            var produtoRepositorySubstitute = Substitute.For<IProdutoRepository>();
            var loteRepositorySubstitute = Substitute.For<ILoteRepository>();

            loteRepositorySubstitute.GetAll().Returns([]);

            var loteService = new LoteService(loteRepositorySubstitute, produtoRepositorySubstitute);

            // Act / Assert
            Action lotesAction = () => loteService.ListarTodosOsLotes();
            lotesAction.Should().Throw<ArgumentException>().WithMessage("Não há lotes em registro.");
        }

        [Fact]
        public void RemoverLote_DeveRemoverLote_QuandoIdForValido()
        {
            // Arrange
            var lote1 = new Lote()
            {
                Id = 0,
                Validade = DateOnly.Parse("11/03/2036"),
                UnidadesProdutos = 800,
                ProdutoId = 1
            };

            var produtoRepositorySubstitute = Substitute.For<IProdutoRepository>();
            var loteRepositorySubstitute = Substitute.For<ILoteRepository>();

            loteRepositorySubstitute.Delete(0).Returns(lote1);

            var loteService = new LoteService(loteRepositorySubstitute, produtoRepositorySubstitute);

            // Act
            var loteRemovido = loteService.RemoverLote(0);

            // Assert
            loteRemovido.Should().NotBeNull();
            loteRemovido.Id.Should().Be(0);
        }

        [Fact]
        public void RemoverLote_DeveLancarExcecao_QuandoIdNaoForValido()
        {
            // Arrange
            var produtoRepositorySubstitute = Substitute.For<IProdutoRepository>();
            var loteRepositorySubstitute = Substitute.For<ILoteRepository>();

            loteRepositorySubstitute.Delete(1).Returns((Lote)null);

            var loteService = new LoteService(loteRepositorySubstitute, produtoRepositorySubstitute);

            // Act / Assert
            Action loteRemovidoAction = () => loteService.RemoverLote(1);
            loteRemovidoAction.Should().Throw<ArgumentException>().WithMessage("Não existe um lote de ID 1.");
        }

        [Fact]
        public void RemoverLotesVencidos_DeveRemoverLotesVencidos_QuandoHaLotesVencidos()
        {
            // Arrange
            var lote1 = new Lote()
            {
                Id = 0,
                Validade = DateOnly.Parse("11/03/2016"),
                UnidadesProdutos = 500,
                ProdutoId = 3
            };
            var produtoRepositorySubstitute = Substitute.For<IProdutoRepository>();
            var loteRepositorySubstitute = Substitute.For<ILoteRepository>();

            loteRepositorySubstitute.GetAll().Returns([lote1]);
            loteRepositorySubstitute.Delete(lote1.Id).Returns(lote1);
            var loteService = new LoteService(loteRepositorySubstitute, produtoRepositorySubstitute);

            // Act
            var loteVencido = loteService.RemoverLotesVencidos();

            // Assert
            loteVencido.Should().NotBeNull();
            loteVencido.Should().HaveCount(1);
            loteVencido.ElementAt(0).Id.Should().Be(0);
        }

        [Fact]
        public void RemoverLotesVencidos_DeveLancarExcecao_QuandoNaoHaLotesVencidos()
        {
            // Arrange
            var produtoRepositorySubstitute = Substitute.For<IProdutoRepository>();
            var loteRepositorySubstitute = Substitute.For<ILoteRepository>();

            loteRepositorySubstitute.GetAll().Returns([]);
            var loteService = new LoteService(loteRepositorySubstitute, produtoRepositorySubstitute);

            // Act / Assert
            Action lotesVencidosAction = () => loteService.RemoverLotesVencidos();
            lotesVencidosAction.Should().Throw<ArgumentException>().WithMessage("Não existem lotes vencidos para serem descartados.");
        }
    }
}
