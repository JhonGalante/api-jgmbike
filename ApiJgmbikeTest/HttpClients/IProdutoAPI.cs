using ApiJgmbikeTest.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiJgmbikeTest.HttpClients
{
    public interface IProdutoAPI
    {
        [Get("/api/Produtos")]
        Task<ApiResponse<IEnumerable<Produto>>> GetAllAsync();

        [Get("/api/Produtos/{id}")]
        Task<ApiResponse<Produto>> GetByIdAsync(int id);

        [Get("/api/Produtos/ProdutosCategorias")]
        Task<ApiResponse<Produto>> GetAllProdutosCategoriasAsync();

        [Get("/api/Produtos/ProdutosPorCategoria/{id}")]
        Task<ApiResponse<Produto>> GetProdutosPorCategoriaAsync(int id);
    }
}
