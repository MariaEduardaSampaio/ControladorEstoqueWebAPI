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

        private string GerarCodigo(DateOnly dataValidade, string nomeProduto)
        {
            string dia = dataValidade.Day.ToString("00");
            string mes = dataValidade.Month.ToString("00");
            string ano = dataValidade.Year.ToString().Substring(2, 2);

            string duasIniciais = nomeProduto.Length >= 2 ? nomeProduto.Substring(0, 2).ToUpper() : nomeProduto.ToUpper();

            int randomSerie = new Random().Next(100, 1000);
            string codigo = $"{dia}{mes}{ano}{duasIniciais}{randomSerie}";

            if (codigo.Length > 11)
                codigo = codigo.Substring(0, 11);

            return codigo;
        }

        private DateOnly ConverterDataValidade(string validadeString)
        {
            if (!DateOnly.TryParse(validadeString, out DateOnly validade))
                validade = DateOnly.MinValue;

            if (validade == DateOnly.MinValue)
                throw new ArgumentException("Data de validade inserida não está em um formato válido.");

            return validade;
        }

        public Lote AdicionarLote(LoteRequest request)
        {
            var produto = _produtoRepository.GetByID(request.ProdutoId);

            if (produto is not null)
            {
                var validade = ConverterDataValidade(request.DataValidade);

                var lote = new Lote()
                {
                    UnidadesProdutos = request.UnidadesProdutos,
                    Validade = validade,
                    Codigo = GerarCodigo(validade, produto.Nome),
                    Produto = produto
                };

                _loteRepository.Create(lote);

                return lote;
            }
            else
                throw new ArgumentException($"Não existe um produto de ID {request.ProdutoId}.");
        }

        public Lote AtualizarLote(LoteRequest request, int id)
        {
            var lote = _loteRepository.GetByID(id);
            var produto = _produtoRepository.GetByID(request.ProdutoId);

            if (lote is not null)
            {
                if (produto is not null)
                {
                    lote.UnidadesProdutos = request.UnidadesProdutos;
                    lote.Validade = ConverterDataValidade(request.DataValidade);
                    lote.Codigo = GerarCodigo(lote.Validade, produto.Nome);
                    lote.Produto = produto;
                    lote.ProdutoId = produto.Id;

                    _loteRepository.Update(lote);

                    return lote;
                }
                else
                    throw new ArgumentException($"Não existe um produto de ID {request.ProdutoId}.");
            }
            else
                throw new ArgumentException($"Não existe um lote de ID {id}.");
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

        public Lote RemoverLote(int idLote)
        {
            var lote = _loteRepository.Delete(idLote);
            if (lote is not null)
                return lote;
            else
                throw new ArgumentException($"Não existe um lote de ID {idLote}.");
        }

        public IEnumerable<Lote> RemoverLotesVencidos()
        {
            var lotes = _loteRepository.GetAll().Where(l => l.Validade <= DateOnly.FromDateTime(DateTime.Now.Date));

            if (lotes.Any())
            {
                foreach (var lote in lotes)
                    _loteRepository.Delete(lote.Id);
                return lotes;
            }
            else
                throw new ArgumentException("Não existem lotes vencidos para serem descartados.");
        }
    }
}