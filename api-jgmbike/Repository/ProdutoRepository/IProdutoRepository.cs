using api_jgmbike.DTOs;
using api_jgmbike.Models;
using System.Collections.Generic;

namespace api_jgmbike.Repository.ProdutoRepository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        IEnumerable<ProdutoDTO> GetProdutosCategorias();
    }
}
