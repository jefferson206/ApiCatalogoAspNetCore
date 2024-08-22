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
        private string _notFoundMessage = "Categoria não encontrada....";

        public CategoriasSincronoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("produtos")]
        public ActionResult<Categoria> GetAllCategoriasProdutos() 
        {
            var categorias = _context.Categorias.Include(c => c.Produtos).AsNoTracking().ToList();
            try
            {
                if (categorias is null)
                {
                    return NotFound(_notFoundMessage);
                }
                return Ok(categorias);
            }
            catch (Exception)
            {
                return InternalErrorMessage();
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> GetAllSincrono()
        {
            var categorias = _context.Categorias.AsNoTracking().ToList();
            try
            {
                if (categorias is null)
                {
                    return NotFound(_notFoundMessage);
                }
                return Ok(categorias);

            }
            catch (Exception)
            {
                return InternalErrorMessage();
            }

        }

        [HttpGet("{id:int:min(1)}", Name = "GetByIdSincrono")]
        public ActionResult<Categoria> GetByIdSincrono(int id)
        {
            var categoria = _context.Categorias.AsNoTracking().FirstOrDefault(c => c.CategoriaId == id);
            try
            {
                if (categoria == null)
                {
                    return NotFound(_notFoundMessage);
                }
                return Ok(categoria);
            }
            catch (Exception)
            {
                return InternalErrorMessage();
            }

        }

        [HttpPost]
        public ActionResult<Categoria> PostCategoriaSincrona(Categoria categoria)
        {
            try
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
            catch (Exception)
            {
                return InternalErrorMessage();
            }
        }

        [HttpPut("{id:int:min(1)}")]
        public ActionResult<Categoria> PutCategoriaSincrona(int id, Categoria categoria)
        {
            try
            {
                if (id != categoria.CategoriaId)
                {
                    return BadRequest();
                }
                _context.Entry(categoria).State = EntityState.Modified;

                var catContext = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
                if (catContext == null)
                {
                    return NotFound(_notFoundMessage);
                }
                _context.SaveChanges();
                return Ok(catContext);
            }
            catch (Exception)
            {
                return InternalErrorMessage();
            }
        }

        [HttpDelete("{id:int:min(1)}")]
        public ActionResult<Categoria> DeleteCategoriaSincrona(int id)
        {
            try
            {
                var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
                if (categoria == null)
                {
                    return NotFound(_notFoundMessage);
                }
                _context.Categorias.Remove(categoria);
                _context.SaveChanges();
                return Ok($"Categoria {id} removido com sucesso...");
            }
            catch (Exception)
            {
                return InternalErrorMessage();
            }
        }

        private ActionResult InternalErrorMessage()
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação...");
        }

    }
}
