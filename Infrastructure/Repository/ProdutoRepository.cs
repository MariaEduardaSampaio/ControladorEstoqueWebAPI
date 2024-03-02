using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Context;

namespace Infrastructure.Repository
{
    internal class ProdutoRepository : IProdutoRepository
    {
        protected readonly ControladorEstoqueContext _controladorEstoqueContext;

        public ProdutoRepository(ControladorEstoqueContext controladorEstoqueContext)
        {
            _controladorEstoqueContext = controladorEstoqueContext;
        }
        public int Create(Produto produto)
        {
            _controladorEstoqueContext.Produtos.Add(produto);
            SaveChanges();
            return produto.Id;
        }

        public Produto? Delete(int id)
        {
            var produto = _controladorEstoqueContext.Produtos.Find(id);

            if (produto is not null)
                _controladorEstoqueContext.Produtos.Remove(produto);

            SaveChanges();

            return produto;
        }

        public IEnumerable<Produto> GetAll()
        {
            return _controladorEstoqueContext.Produtos.ToList();
        }

        public Produto? GetByID(int id)
        {
            return _controladorEstoqueContext.Produtos.Find(id);
        }

        public Produto? GetByName(string name)
        {
            return _controladorEstoqueContext.Produtos.FirstOrDefault(p => p.Nome == name);
        }

        public void Update(Produto produto)
        {
            _controladorEstoqueContext.Produtos.Update(produto);
            SaveChanges();
        }

        private void SaveChanges()
        {
            _controladorEstoqueContext.SaveChanges();
        }
    }
}
