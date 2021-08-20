using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_jgmbike.Context;
using api_jgmbike.Models;
using api_jgmbike.Repository;

namespace api_jgmbike.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ProdutosController : ControllerBase
    {
        private readonly IRepository<Produto> _repo;

        public ProdutosController(IRepository<Produto> repo)
        {
            _repo = repo;
        }

        // GET: api/Produtos
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> GetProdutos()
        {
            return _repo.Get().ToList();
        }

        // GET: api/Produtos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
            var produto = await _repo.GetById(opt => opt.Id == id);

            if (produto == null)
            {
                return NotFound();
            }

            return produto;
        }

        // PUT: api/Produtos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutProduto(int id, Produto produto)
        {
            if (id != produto.Id)
            {
                return BadRequest();
            }

            try
            {
                _repo.Update(produto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Produtos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Produto> PostProduto(Produto produto)
        {
            _repo.Add(produto);
            return CreatedAtAction("GetProduto", new { id = produto.Id }, produto);
        }

        // DELETE: api/Produtos/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProduto(int id)
        {
            var produto = _repo.GetById(prod => prod.Id == id).Result;

            if (produto == null)
            {
                return NotFound();
            }

            _repo.Delete(produto);

            return NoContent();
        }

        private bool ProdutoExists(int id)
        {
            return _repo.EntityExists(id);
        }
    }
}
