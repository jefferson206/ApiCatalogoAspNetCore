using ApiCatalogo.Models;

namespace ApiCatalogo.Repositories.Interfaces;

public interface IProdutoRepositoryOld
{
    IQueryable<Produto> GetProdutos();
    Produto GetProduto(int id);
    Produto Create(Produto produto);
    bool Update(Produto produto);
    bool Delete(int id);

}
