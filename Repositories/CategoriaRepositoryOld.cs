using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repositories;

public class CategoriaRepositoryOld : ICategoriaRepositoryOld
{
    private readonly AppDbContext _context;

    public CategoriaRepositoryOld(AppDbContext context)
    {
        _context = context;
    }

    public Categoria GetCategoria(int id)
    {
        return _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
    }

    public IEnumerable<Categoria> GetCategorias()
    {
        return _context.Categorias.ToList();
    }

    public IQueryable<Categoria> GetCategoriasQueryable()
    {
        return _context.Categorias;
    }

    public Categoria Create(Categoria categoria)
    {
        if (categoria == null) 
        { 
            throw new ArgumentNullException(nameof(categoria));
        }
        _context.Categorias.Add(categoria);
        _context.SaveChanges();
        return categoria;
    }

    public Categoria Delete(int id)
    {
        var categoria = _context.Categorias.Find(id);
        if (categoria == null)
        {
            throw new ArgumentNullException(nameof(categoria));
        }
        _context.Categorias.Remove(categoria);
        _context.SaveChanges();
        return categoria;
    }

    public Categoria Update(Categoria categoria)
    {

        if (categoria == null)
        {
            throw new ArgumentNullException(nameof(categoria));
        }
        var categoriaContext = _context.Categorias.Find(categoria.CategoriaId);

        _context.Entry(categoriaContext).State = EntityState.Modified;
        _context.SaveChanges();
        return categoria;
    }
}
