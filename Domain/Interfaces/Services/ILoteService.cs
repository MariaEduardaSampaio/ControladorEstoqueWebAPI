using Domain.Entities;
using Domain.Requests;

namespace Domain.Interfaces.Services
{
    public interface ILoteService
    {
        Lote AdicionarLote(LoteRequest request);
        IEnumerable<Lote> ListarTodosOsLotes();
        IEnumerable<Lote> ListarLotesPorProduto(int idProduto);
        IEnumerable<Lote> ListarLotesVencidos();
        Lote ListarLotePorCodigo(string codigo);
        Lote ListarLotePorID(int idLote);
        Lote AtualizarLote(LoteRequest request, int id);
        Lote RemoverLote(int idLote);
        IEnumerable<Lote> RemoverLotesVencidos();
    }
}
