using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface ILoteRepository
    {
        int Create(Lote lote);
        IEnumerable<Lote>? GetAll();
        IEnumerable<Lote>? GetAllByProduct(int idProduct);
        Lote? GetByID(int id);
        Lote? GetByCode(string code);
        void Update(Lote lote);
        Lote? Delete(int id);
        IEnumerable<Lote>? DeleteExpiredBatches();

    }
}
