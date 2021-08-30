using api_jgmbike.DTOs;
using api_jgmbike.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_jgmbike.Repository.CategoriaProdutosRepository
{
    public interface ICategoriaProdutosRepository : IRepository<CategoriaProduto>
    {
        IEnumerable<CategoriaProdutoDTO> GetCategorias();
        CategoriaProdutoDTO GetCategoriaById(int id);
    }
}
