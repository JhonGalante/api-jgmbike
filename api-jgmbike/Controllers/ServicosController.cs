using api_jgmbike.Context;
using api_jgmbike.DTOs;
using api_jgmbike.Models;
using api_jgmbike.Repository.ServicoRepository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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
            try
            {
                return _repo.GetServicos().ToList(); 
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        // GET: api/Servicos/5
        /// <summary>
        /// Retorna um serviço pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Objeto de Serviço</returns>
        [HttpGet("{id}")]
        public ActionResult<ServicoDTO> GetServico(int id)
        {
            try
            {
                var servico = _repo.GetServicoById(id);

                if (servico == null)
                {
                    return NotFound();
                }

                return servico;
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        // PUT: api/Servicos/5
        /// <summary>
        /// Atualiza um registro de serviço no banco
        /// </summary>
        /// <param name="id"></param>
        /// <param name="servico"></param>
        /// <returns>ActionResult</returns>
        [HttpPut("{id}")]
        public IActionResult PutServico(int id, Servico servico)
        {
            if (id != servico.Id)
            {
                return BadRequest();
            }

            try
            {
                _repo.Update(servico);
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
        public ActionResult<Servico> PostServico(Servico servico)
        {
            _repo.Add(servico);
            return CreatedAtAction("GetServico", new { id = servico.Id }, servico);
        }

        // DELETE: api/Servicos/5
        /// <summary>
        /// Deleta um registro de serviço no banco
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ActionResult</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteServico(int id)
        {
            var servico = _repo.GetById(svc => svc.Id == id).Result;
            if (servico == null)
            {
                return NotFound();
            }

            _repo.Delete(servico);

            return NoContent();
        }

        private bool ServicoExists(int id)
        {
            return _context.Servicos.Any(e => e.Id == id);
        }
    }
}
