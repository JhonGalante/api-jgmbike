using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_jgmbike.Context;
using api_jgmbike.Models;
using Microsoft.AspNetCore.Cors;
using api_jgmbike.Repository.CategoriaProdutosRepository;
using api_jgmbike.DTOs;

namespace api_jgmbike.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [EnableCors("PoliticaJGMBike")]
    public class CategoriaProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ICategoriaProdutosRepository _repo;

        public CategoriaProdutosController(AppDbContext context, ICategoriaProdutosRepository repo)
        {
            _context = context;
            _repo = repo;
        }

        // GET: api/CategoriaProdutos
        /// <summary>
        /// Retorna todas as categorias do banco
        /// </summary>
        /// <returns>Objetos de CategoriaProdutoDTO</returns>
        [HttpGet]
        public ActionResult<IEnumerable<CategoriaProdutoDTO>> GetCategoriasProdutos()
        {
            return _repo.GetCategorias().ToList();
        }

        // GET: api/CategoriaProdutos/5
        /// <summary>
        /// Retorna uma categoria pela Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Objeto de CategoriaProdutoDTO</returns>
        [HttpGet("{id}")]
        public ActionResult<CategoriaProdutoDTO> GetCategoriaProduto(int id)
        {
            var categoriaProduto = _repo.GetCategoriaById(id);

            if (categoriaProduto == null)
            {
                return NotFound();
            }

            return categoriaProduto;
        }

        // PUT: api/CategoriaProdutos/5
        /// <summary>
        /// Atualiza um registro de categoria no banco
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoriaProduto"></param>
        /// <returns>Action Result</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoriaProduto(int id, CategoriaProduto categoriaProduto)
        {
            if (id != categoriaProduto.Id)
            {
                return BadRequest();
            }

            _context.Entry(categoriaProduto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaProdutoExists(id))
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

        // POST: api/CategoriaProdutos
        /// <summary>
        /// Insere um novo registro de categoria
        /// </summary>
        /// <param name="categoriaProduto"></param>
        /// <returns>Objeto inserido no banco</returns>
        [HttpPost]
        public async Task<ActionResult<CategoriaProduto>> PostCategoriaProduto(CategoriaProduto categoriaProduto)
        {
            _context.CategoriaProdutos.Add(categoriaProduto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoriaProduto", new { id = categoriaProduto.Id }, categoriaProduto);
        }

        // DELETE: api/CategoriaProdutos/5
        /// <summary>
        /// Deleta um registro de categoria
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Action Result</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoriaProduto(int id)
        {
            var categoriaProduto = await _context.CategoriaProdutos.FindAsync(id);
            if (categoriaProduto == null)
            {
                return NotFound();
            }

            _context.CategoriaProdutos.Remove(categoriaProduto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriaProdutoExists(int id)
        {
            return _context.CategoriaProdutos.Any(e => e.Id == id);
        }
    }
}
