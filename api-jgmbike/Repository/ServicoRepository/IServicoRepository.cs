using api_jgmbike.DTOs;
using api_jgmbike.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_jgmbike.Repository.ServicoRepository
{
    public interface IServicoRepository : IRepository<Servico>
    {
        IEnumerable<ServicoDTO> GetServicos();
        ServicoDTO GetServicoById(int id);
    }
}
