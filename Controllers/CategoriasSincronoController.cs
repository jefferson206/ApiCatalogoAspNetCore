using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasSincronoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriasSincronoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("produtos")]
        public ActionResult<Categoria> GetAllCategoriasProdutos() 
        {
            var categorias = _context.Categorias.Include(c => c.Produtos).AsNoTracking().ToList();
            if (categorias is null)
            {
                return NotFound("Categoria não encontrada....");
            }
            return Ok(categorias);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> GetAllSincrono()
        {
            var categorias = _context.Categorias.AsNoTracking().ToList();
            if (categorias is null)
            {
                return NotFound("Categoria não encontrada....");
            }
            return Ok(categorias);
        }

        [HttpGet("{id:int}", Name = "GetByIdSincrono")]
        public ActionResult<Categoria> GetByIdSincrono(int id)
        {
            var categoria = _context.Categorias.AsNoTracking().FirstOrDefault(c => c.CategoriaId == id);
            if (categoria == null) 
            { 
                return NotFound("Categoria não encontrada...");
            }
            return Ok(categoria);
        }

        [HttpPost]
        public ActionResult<Categoria> PostCategoriaSincrona(Categoria categoria)
        {
            if (categoria == null)
            {
                return BadRequest();
            }
            if (categoria.CategoriaId != null)
            {
                var catContext = _context.Categorias.FirstOrDefault(c => c.CategoriaId == categoria.CategoriaId);
                if (catContext != null)
                {
                    return BadRequest();
                }
            }
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            return new CreatedAtRouteResult("GetByIdSincrono", new { id = categoria.CategoriaId }, categoria);
        }

        [HttpPut("{id:int}")]
        public ActionResult<Categoria> PutCategoriaSincrona(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest();
            }
            _context.Entry(categoria).State = EntityState.Modified;

            var catContext = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
            if (catContext == null)
            {
                return NotFound("Categoria não encontrada...");
            }
            _context.SaveChanges();
            return Ok(catContext);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Categoria> DeleteCategoriaSincrona(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
            if (categoria == null)
            {
                return NotFound("Categoria não encontrada");
            }
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
            return Ok($"Categoria {id} removido com sucesso...");
        }
    }
}
