using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Context;

namespace Infrastructure.Repository
{
    public class LoteRepository : ILoteRepository
    {
        protected readonly ControladorEstoqueContext _controladorEstoqueContext;

        public LoteRepository(ControladorEstoqueContext controladorEstoqueContext)
        {
            _controladorEstoqueContext = controladorEstoqueContext;
        }

        public int Create(Lote lote)
        {
            _controladorEstoqueContext.Lotes.Add(lote);
            SaveChanges();
            return lote.Id;
        }

        public Lote? Delete(int id)
        {
            var lote = _controladorEstoqueContext.Lotes.Find(id);

            if (lote is not null)
                _controladorEstoqueContext.Lotes.Remove(lote);

            SaveChanges();

            return lote;
        }

        public IEnumerable<Lote>? DeleteExpiredBatches()
        {
            var lotesVencidos = _controladorEstoqueContext.Lotes
                .Where(l => l.Validade <= DateOnly.FromDateTime(DateTime.Now.Date));

            foreach (var lote in lotesVencidos)
                _controladorEstoqueContext.Lotes.Remove(lote);

            SaveChanges();

            return lotesVencidos;
        }

        public IEnumerable<Lote>? GetAll()
        {
            return _controladorEstoqueContext.Lotes.ToList();
        }

        public IEnumerable<Lote>? GetAllByProduct(int idProduct)
        {
            IEnumerable<Lote> lotesDeProduto = [];
            var produto = _controladorEstoqueContext.Produtos.Find(idProduct);

            if (produto is not null)
                lotesDeProduto = _controladorEstoqueContext.Lotes.Where(l => l.Produto == produto).ToList();
            
            return lotesDeProduto;
        }

        public Lote? GetByCode(string code)
        {
            return _controladorEstoqueContext.Lotes.FirstOrDefault(l => l.Codigo == code);
        }

        public Lote? GetByID(int id)
        {
            return _controladorEstoqueContext.Lotes.Find(id);
        }

        public void Update(Lote lote)
        {
            _controladorEstoqueContext.Update(lote);
            SaveChanges();
        }
        private void SaveChanges()
        {
            _controladorEstoqueContext.SaveChanges();
        }
    }
}
