using Domain.Entities;
using Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IProdutoService
    {
        Produto AdicionarProduto(ProdutoRequest request);
        IEnumerable<Produto> ListarTodosOsProdutos();
        IEnumerable<Produto> ListarProdutosPorNome(string nome);
        Produto ListarProdutoPorID(int idProduto);
        Produto AtualizarProduto(ProdutoRequest request, int idProduto);
        Produto RemoverProduto(int idProduto);
    }
}
