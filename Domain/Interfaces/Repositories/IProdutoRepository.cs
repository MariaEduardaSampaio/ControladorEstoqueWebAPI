using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IProdutoRepository
    {
        int Create(Produto produto);
        IEnumerable<Produto> GetAll();
        Produto? GetByID(int id);
        Produto? GetByName(string name);
        void Update(Produto produto);
        Produto? Delete(int id);
    }
}
