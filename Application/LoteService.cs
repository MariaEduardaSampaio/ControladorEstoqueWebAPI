using Azure.Core;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Requests;

namespace ControladorEstoqueWebAPI.Services
{
    public class LoteService : ILoteService
    {
        private readonly ILoteRepository _loteRepository;
        private readonly IProdutoRepository _produtoRepository;
        public LoteService(ILoteRepository loteRepository, IProdutoRepository produtoRepository)
        {
            _loteRepository = loteRepository;
            _produtoRepository = produtoRepository;

        }
        public Lote Adicionar(LoteRequest request)
        {
            var produto = _produtoRepository.GetByID(request.ProdutoId);

            if (produto is not null)
            {
                var lote = new Lote()
                {
                    Codigo = request.Codigo,
                    UnidadesProdutos = request.UnidadesProdutos,
                    Validade = request.Validade,
                    Produto = produto
                };

                _loteRepository.Create(lote);

                return lote;
            }
            else
                throw new ArgumentException($"Não existe um produto de ID {request.ProdutoId}.");
        }

        public Lote Atualizar(LoteRequest request, int id)
        {
            var produto = _produtoRepository.GetByID(request.ProdutoId);

            if (produto is not null)
            {
                var lote = new Lote()
                {
                    Id = id,
                    Codigo = request.Codigo,
                    UnidadesProdutos = request.UnidadesProdutos,
                    Validade = request.Validade,
                    Produto = produto,
                    ProdutoId = produto.Id
                };

                _loteRepository.Create(lote);

                return lote;
            }
            else
                throw new ArgumentException($"Não existe um produto de ID {request.ProdutoId}.");
        }

        public Lote ListarLotePorCodigo(string codigo)
        {
            var lote = _loteRepository.GetByCode(codigo);
            if (lote is not null)
                return lote;
            else
                throw new ArgumentException($"Não existe um lote de código {codigo}.");
        }

        public Lote ListarLotePorID(int idLote)
        {
            var lote = _loteRepository.GetByID(idLote);
            if (lote is not null)
                return lote;
            else
                throw new ArgumentException($"Não existe um lote de ID {idLote}.");
        }

        public IEnumerable<Lote> ListarLotesPorProduto(int idProduto)
        {
            var produto = _produtoRepository.GetByID(idProduto);

            if (produto is not null)
            { 
                var lotes = _loteRepository.GetAllByProduct(idProduto);
                if (lotes.Any())
                    return lotes;
                else
                    throw new ArgumentException("Não existem lotes de produtos com este ID.");
            }
            else
                throw new ArgumentException($"Não existe um produto de ID {idProduto}.");
        }

        public IEnumerable<Lote> ListarLotesVencidos()
        {
            var lotes = _loteRepository.GetAll().Where(l => l.Validade <= DateOnly.FromDateTime(DateTime.Now.Date));
            if (lotes.Any())
                return lotes;
            else
                throw new ArgumentException("Não há lotes vencidos em registro.");
        }

        public IEnumerable<Lote> ListarTodosOsLotes()
        {
            var lotes = _loteRepository.GetAll();
            if (lotes.Any())
                return lotes;
            else
                throw new ArgumentException("Não há lotes em registro.");
        }

        public Lote Remover(int idLote)
        {
            var lote = _loteRepository.Delete(idLote);
            if (lote is not null)
                return lote;
            else
                throw new ArgumentException($"Não existe um lote de ID {idLote}.");
        }

        public IEnumerable<Lote> RemoverLotesVencidos()
        {
            var lotes = ListarLotesVencidos();

            foreach (var lote in lotes)
                Remover(lote.Id);

            return lotes;
        }
    }
}
