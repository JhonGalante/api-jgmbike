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
using api_jgmbike.DTOs;
using api_jgmbike.Repository.ServicoRepository;

namespace api_jgmbike.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [EnableCors("PoliticaJGMBike")]
    public class ServicosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IServicoRepository _repo;

        public ServicosController(AppDbContext context, IServicoRepository repo)
        {
            _context = context;
            _repo = repo;
        }

        // GET: api/Servicos
        /// <summary>
        /// Retorna todos os serviços
        /// </summary>
        /// <returns>Objetos de Serviço</returns>
        [HttpGet]
        public ActionResult<IEnumerable<ServicoDTO>> GetServicos()
        {
            return _repo.GetServicos().ToList(); 
        }

        // GET: api/Servicos/5
        /// <summary>
        /// Retorna um serviço pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Objeto de Serviço</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Servico>> GetServico(int id)
        {
            var servico = await _context.Servicos.FindAsync(id);

            if (servico == null)
            {
                return NotFound();
            }

            return servico;
        }

        // PUT: api/Servicos/5
        /// <summary>
        /// Atualiza um registro de serviço no banco
        /// </summary>
        /// <param name="id"></param>
        /// <param name="servico"></param>
        /// <returns>ActionResult</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServico(int id, Servico servico)
        {
            if (id != servico.Id)
            {
                return BadRequest();
            }

            _context.Entry(servico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicoExists(id))
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

        // POST: api/Servicos
        /// <summary>
        /// Insere um novo registro de serviço no banco
        /// </summary>
        /// <param name="servico"></param>
        /// <returns>Objeto de serviço inserido</returns>
        [HttpPost]
        public async Task<ActionResult<Servico>> PostServico(Servico servico)
        {
            _context.Servicos.Add(servico);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetServico", new { id = servico.Id }, servico);
        }

        // DELETE: api/Servicos/5
        /// <summary>
        /// Deleta um registro de serviço no banco
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ActionResult</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServico(int id)
        {
            var servico = await _context.Servicos.FindAsync(id);
            if (servico == null)
            {
                return NotFound();
            }

            _context.Servicos.Remove(servico);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServicoExists(int id)
        {
            return _context.Servicos.Any(e => e.Id == id);
        }
    }
}
