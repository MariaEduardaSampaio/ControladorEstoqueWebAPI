﻿using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IProdutoRepository
    {
        Produto? Create(Produto produto);
        IEnumerable<Produto>? GetAll();
        Produto? GetByID(int id);
        IEnumerable<Produto>? GetByName(string name);
        void Update(Produto produto);
        Produto? Delete(int id);
    }
}
