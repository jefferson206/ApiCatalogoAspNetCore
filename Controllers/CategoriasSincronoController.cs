using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasSincronoController : ControllerBase
    {
        private readonly ICategoriaRepositoryOld _repository;
        private string _notFoundMessage = "Categoria não encontrada....";

        public CategoriasSincronoController(ICategoriaRepositoryOld repository)
        {
            _repository = repository;
        }

        [HttpGet("produtos")]
        public ActionResult<Categoria> GetAllCategoriasProdutos()
        {
            var categorias = _repository.GetCategoriasQueryable().Include(c => c.Produtos).AsNoTracking().ToList();
            if (categorias is null)
            {
                return NotFound(_notFoundMessage);
            }
            return Ok(categorias);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> GetAllSincrono()
        {
            var categorias = _repository.GetCategorias();
            if (categorias is null)
            {
                return NotFound(_notFoundMessage);
            }
            return Ok(categorias);
        }

        [HttpGet("{id:int:min(1)}", Name = "GetByIdSincrono")]
        public ActionResult<Categoria> GetByIdSincrono(int id)
        {
            var categoria = _repository.GetCategoria(id);
            if (categoria == null)
            {
                return NotFound(_notFoundMessage);
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
                var catContext = _repository.GetCategoria(categoria.CategoriaId);
                if (catContext != null)
                {
                    return BadRequest();
                }
            }
            var categoriaCriada = _repository.Create(categoria);
            return new CreatedAtRouteResult("GetByIdSincrono", new { id = categoriaCriada.CategoriaId }, categoriaCriada);

        }

        [HttpPut("{id:int:min(1)}")]
        public ActionResult<Categoria> PutCategoriaSincrona(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest();
            }
            var catContext = _repository.GetCategoria(categoria.CategoriaId);
            if (catContext == null)
            {
                return NotFound(_notFoundMessage);
            }
            var categoriaUpdated = _repository.Update(categoria);
            return Ok(categoriaUpdated);
        }

        [HttpDelete("{id:int:min(1)}")]
        public ActionResult<Categoria> DeleteCategoriaSincrona(int id)
        {
            var catContext = _repository.GetCategoria(id);
            if (catContext == null)
            {
                return NotFound(_notFoundMessage);
            }
            _repository.Delete(id);
            return Ok($"Categoria {id} removido com sucesso...");
        }
    }
}
