using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _context;

    public ProdutoRepository(AppDbContext context)
    {
        _context = context;
    }

    public Produto GetProduto(int id)
    {
        return _context.Produtos.FirstOrDefault(c => c.ProdutoId == id);
    }

    public IQueryable<Produto> GetProdutos()
    {
        return _context.Produtos;
    }

    public Produto Create(Produto produto)
    {
        if (produto == null) 
        { 
            throw new ArgumentNullException(nameof(produto));
        }
        _context.Produtos.Add(produto);
        _context.SaveChanges();
        return produto;
    }

    public bool Delete(int id)
    {
        var Produto = _context.Produtos.Find(id);
        if (Produto == null)
        {
            throw new ArgumentNullException(nameof(Produto));
        }
        var produtoContext = _context.Produtos.Find(id);
        if (produtoContext is not null)
        {
            _context.Produtos.Remove(Produto);
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    public bool Update(Produto produto)
    {

        if (produto == null)
        {
            throw new ArgumentNullException(nameof(produto));
        }
        var produtoContext = _context.Produtos.Any(p => p.ProdutoId == produto.ProdutoId);
        if (produtoContext)
        {
            _context.Produtos.Update(produto);
            _context.SaveChanges();
            return true;
        }
        return false;
    }

}
