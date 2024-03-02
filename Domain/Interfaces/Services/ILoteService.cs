using Domain.Entities;
using Domain.Requests;

namespace Domain.Interfaces.Services
{
    public interface ILoteService
    {
        Lote Adicionar(LoteRequest request);
        IEnumerable<Lote> ListarTodosOsLotes();
        IEnumerable<Lote> ListarLotesPorProduto(int idProduto);
        IEnumerable<Lote> ListarLotesVencidos();
        Lote ListarLotePorCodigo(string codigo);
        Lote ListarLotePorID(int idLote);
        Lote Atualizar(LoteRequest request);
        Lote Remover(int idLote);
        IEnumerable<Lote> RemoverLotesVencidos();
    }
}
