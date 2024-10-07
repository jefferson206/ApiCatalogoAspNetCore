using ApiCatalogo.Models;

namespace ApiCatalogo.Repositories.Interfaces;

public interface ICategoriaRepositoryOld
{
    IEnumerable<Categoria> GetCategorias();
    IQueryable<Categoria> GetCategoriasQueryable();
    Categoria GetCategoria(int id);
    Categoria Create(Categoria categoria);
    Categoria Update(Categoria categoria);
    Categoria Delete(int id);

}
