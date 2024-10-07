using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosSincronoController : ControllerBase
    {
        private readonly IProdutoRepository _repository;

        public ProdutosSincronoController(IProdutoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> GetProdutosAllSincrono()
        {
            var produtos = _repository.GetProdutos().AsNoTracking().ToList();
            if (produtos is null)
            {
                return NotFound("Produto não encontrado....");
            }
            return Ok(produtos);
        }

        [HttpGet("{id:int}", Name = "obterProdutoSincrono")]
        public ActionResult<Produto> GetByIdSincrono(int id)
        {
            var produto = _repository.GetProduto(id);
            if (produto is null)
            {
                return NotFound("Produto não encontrado...");
            }
            return Ok(produto);
        }

        [HttpPost]
        public ActionResult<Produto> PostSincrono(Produto produto)
        {
            if (produto is null)
            {
                return BadRequest();
            }
            var produtoCriado = _repository.Create(produto);
            return new CreatedAtRouteResult("obterProdutoSincrono", new { id = produtoCriado.ProdutoId }, produtoCriado);
        }

        [HttpPut("{id:int}")]
        public ActionResult<Produto> PutSincrono(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }
            bool isValid = _repository.Update(produto);
            if (isValid)
            {
                return Ok(produto);
            }
            return StatusCode(500, $"Falha ao atualizar o produto {id}.");
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteSincrono(int id)
        {
            var produto = _repository.GetProduto(id);
            if (produto is null)
            {
                return NotFound("Produto não encontrado....");
            }
            bool isValid = _repository.Delete(produto.ProdutoId);
            if (isValid)
            {
                return Ok($"Produto {id} removido com sucesso...");
            }
            return StatusCode(500, $"Falha ao excluir o produto {id}.");
        }
    }
}
