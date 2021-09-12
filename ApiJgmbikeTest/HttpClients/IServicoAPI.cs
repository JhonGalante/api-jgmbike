using ApiJgmbikeTest.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiJgmbikeTest.HttpClients
{
    public interface IServicoAPI
    {
        [Get("/api/Servicos")]
        Task<ApiResponse<IEnumerable<Servico>>> GetAllAsync();

        [Get("/api/Servicos/{id}")]
        Task<ApiResponse<CategoriaProduto>> GetByIdAsync(int id);
    }
}
