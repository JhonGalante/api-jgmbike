using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_jgmbike.Context;
using api_jgmbike.Models;

namespace api_jgmbike.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriaProdutosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/CategoriaProdutos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaProduto>>> GetCategoriasProdutos()
        {
            return await _context.CategoriasProdutos.ToListAsync();
        }

        // GET: api/CategoriaProdutos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaProduto>> GetCategoriaProduto(int id)
        {
            var categoriaProduto = await _context.CategoriasProdutos.FindAsync(id);

            if (categoriaProduto == null)
            {
                return NotFound();
            }

            return categoriaProduto;
        }

        // PUT: api/CategoriaProdutos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoriaProduto>> PostCategoriaProduto(CategoriaProduto categoriaProduto)
        {
            _context.CategoriasProdutos.Add(categoriaProduto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoriaProduto", new { id = categoriaProduto.Id }, categoriaProduto);
        }

        // DELETE: api/CategoriaProdutos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoriaProduto(int id)
        {
            var categoriaProduto = await _context.CategoriasProdutos.FindAsync(id);
            if (categoriaProduto == null)
            {
                return NotFound();
            }

            _context.CategoriasProdutos.Remove(categoriaProduto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriaProdutoExists(int id)
        {
            return _context.CategoriasProdutos.Any(e => e.Id == id);
        }
    }
}
