using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Requests;

namespace ControladorEstoqueWebAPI.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public Produto AdicionarProduto(ProdutoRequest request)
        {
            var produto = new Produto()
            {
                Nome = request.Nome,
                Lotes = new()
            };

            _produtoRepository.Create(produto);
            return produto;
        }

        public Produto AtualizarProduto(ProdutoRequest request, int idProduto)
        {
            var produto = _produtoRepository.GetByID(idProduto);

            if (produto is not null)
            {
                produto.Nome = request.Nome;
                _produtoRepository.Update(produto);
                return produto;
            }
            else
                throw new ArgumentException($"Não existe um produto com ID {idProduto}.");
        }

        public Produto ListarProdutoPorID(int idProduto)
        {
            var produto = _produtoRepository.GetByID(idProduto);
            if (produto is not null)
                return produto;
            else
                throw new ArgumentException($"Não existe um produto com ID {idProduto}.");
        }

        public IEnumerable<Produto> ListarProdutosPorNome(string nome)
        {
            var produtos = _produtoRepository.GetByName(nome);
            if (produtos.Any())
                return produtos;
            else
                throw new ArgumentException($"Não existem produtos de nome {nome}.");

        }

        public IEnumerable<Produto> ListarTodosOsProdutos()
        {
            var produtos = _produtoRepository.GetAll();
            if (produtos.Any())
                return produtos;
            else
                throw new ArgumentException("Não há produtos registrados.");
        }

        public Produto RemoverProduto(int idProduto)
        {
            var produto = _produtoRepository.GetByID(idProduto);
            if (produto is not null)
            {
                _produtoRepository.Delete(produto.Id);
                return produto;
            }
            else
                throw new ArgumentException($"Não existe um produto com ID {idProduto}.");
        }
    }
}
