using ApiJgmbikeTest.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiJgmbikeTest.HttpClients
{
    public interface ICategoriaProdutoAPI
    {
        [Get("/api/CategoriaProdutos")]
        Task<ApiResponse<IEnumerable<CategoriaProduto>>> GetAllAsync();

        [Get("/api/CategoriaProdutos/{id}")]
        Task<ApiResponse<CategoriaProduto>> GetByIdAsync(int id);
    }
}
