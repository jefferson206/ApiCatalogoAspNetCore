using ApiCatalogo.Context;
using ApiCatalogo.Filters;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;
        private string _notFoundMessage = "Categoria não encontrada....";
        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLogginFilter))]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetAllCategorias()
        {
            var categorias = await _context.Categorias.AsNoTracking().ToListAsync();
            if (categorias == null)
            {
                return NotFound(_notFoundMessage);
            }
            return Ok(categorias);
           
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<Categoria>> GetCategoriaById(int id)
        {
            
            var categoria = await _context.Categorias.AsNoTracking().FirstOrDefaultAsync(c => c.CategoriaId == id);
            if (categoria is null)
            {
                return NotFound(_notFoundMessage);
            }
            return Ok(categoria);
           
        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> PostCategoria(Categoria categoria)
        {
            
            if (categoria is null)
            {
                return BadRequest();
            }
            if (categoria.CategoriaId != null)
            {
                var catContext = await _context.Categorias.FirstOrDefaultAsync(c => c.CategoriaId == categoria.CategoriaId);
                if (catContext != null)
                {
                    return BadRequest();
                }
            }
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCategoriaById", new { id = categoria.CategoriaId }, categoria);

        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult<Categoria>> PutCategoria(int id, Categoria categoria)
        {
            if (categoria is null || categoria.CategoriaId != id)
            {
                return BadRequest();
            }
            _context.Entry(categoria).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(categoria);
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult<string>> DeleteCategoria(int id)
        {
            
            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.CategoriaId == id);
            if (categoria is null)
            {
                return NotFound(_notFoundMessage);
            }
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
            return Ok($"Categoria {id} removida com sucesso...");
            
        }
    }
}
