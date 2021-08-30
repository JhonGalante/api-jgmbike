using api_jgmbike.DTOs;
using api_jgmbike.Models;
using api_jgmbike.Repository;
using api_jgmbike.Repository.ProdutoRepository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_jgmbike.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [EnableCors("PoliticaJGMBike")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _repo;

        public ProdutosController(IProdutoRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Produtos
        /// <summary>
        /// Retorna todos os produtos
        /// </summary>
        /// <returns>Objetos Produtos</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> GetProdutos()
        {
            return _repo.Get().ToList();
        }

        // GET: api/ProdutosCategorias
        /// <summary>
        /// Retorna todos os produtos junto com o nome de sua categoria
        /// </summary>
        /// <returns>Objetos ProdutosDTO</returns>
        [HttpGet("ProdutosCategorias")]
        public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosCategorias()
        {
            return _repo.GetProdutosCategorias().ToList();
        }

        [HttpGet("ProdutosPorCategoria/{id:int}")]
        public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosPorCategoria(int id)
        {
            return _repo.GetProdutosPorCategoria(id).ToList();
        }

        // GET: api/Produtos/5
        /// <summary>
        /// Retorna um produto pelo seu id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Objeto Produto</returns>
        [HttpGet("{id}")]
        public ActionResult<ProdutoDTO> GetProduto(int id)
        {
            var produto = _repo.GetProdutoById(id);

            if (produto == null)
            {
                return NotFound();
            }

            return produto;
        }

        // PUT: api/Produtos/5
        /// <summary>
        /// Atualizar um objeto de produto no banco
        /// </summary>
        /// <param name="id"></param>
        /// <param name="produto"></param>
        /// <returns>ActionResult</returns>
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
        /// <summary>
        /// Insere um novo objeto de Produto no banco
        /// </summary>
        /// <remarks>
        ///     Exemplo de request:
        ///         POST api/Produtos
        ///         {
        ///               "id": 0,
        ///               "nome": "string",
        ///               "descricao": "string",
        ///               "preco": 0,
        ///               "estoque": 0,
        ///               "imagemUrl": "string",
        ///               "categoriaId": 0
        ///         }
        /// </remarks>
        /// <param name="produto"></param>
        /// <returns>Retorna o objeto de produto incluído no banco</returns>
        [HttpPost]
        public ActionResult<Produto> PostProduto(Produto produto)
        {
            _repo.Add(produto);
            return CreatedAtAction("GetProduto", new { id = produto.Id }, produto);
        }

        // DELETE: api/Produtos/5
        /// <summary>
        /// Deletar um objeto de produto do banco
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ActionResult</returns>
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
