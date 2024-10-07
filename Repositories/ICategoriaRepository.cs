using ApiCatalogo.Models;

namespace ApiCatalogo.Repositories;

public interface ICategoriaRepository
{
    IEnumerable<Categoria> GetCategorias();
    IQueryable<Categoria> GetCategoriasQueryable();
    Categoria GetCategoria(int id);
    Categoria Create(Categoria categoria);
    Categoria Update(Categoria categoria);
    Categoria Delete(int id);

}
