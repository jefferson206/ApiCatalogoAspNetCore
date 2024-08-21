using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosSincronoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosSincronoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> GetProdutosAllSincrono()
        {
            var produtos = _context.Produtos.AsNoTracking().ToList();
            if (produtos is null)
            {
                return NotFound("Produto não encontrado....");
            }
            return produtos;
        }

        [HttpGet("{id:int}", Name = "obterProdutoSincrono")]
        public ActionResult<Produto> GetByIdSincrono(int id)
        {
            var produto = _context.Produtos.AsNoTracking().FirstOrDefault(p => p.ProdutoId == id);
            if (produto is null)
            {
                return NotFound("Produto não encontrado...");
            }
            return produto;
        }

        [HttpPost]
        public ActionResult<Produto> PostSincrono(Produto produto)
        {
            if (produto is null)
            {
                return BadRequest();
            }
            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return new CreatedAtRouteResult("obterProdutoSincrono", new { id = produto.ProdutoId }, produto);

        }

        [HttpPut("{id:int}")]
        public ActionResult<Produto> PutSincrono(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }
            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteSincrono(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

            if (produto is null)
            {
                return NotFound("Produto não encontrado....");
            }
            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return Ok($"Produto {id} removido com sucesso...");
        }
    }
}
