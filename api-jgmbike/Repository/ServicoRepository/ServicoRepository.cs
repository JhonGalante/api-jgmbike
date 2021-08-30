using api_jgmbike.Context;
using api_jgmbike.DTOs;
using api_jgmbike.Models;
using System.Collections.Generic;
using System.Linq;

namespace api_jgmbike.Repository.ServicoRepository
{
    public class ServicoRepository : Repository<Servico>, IServicoRepository
    {
        private readonly AppDbContext _context;
        private List<ServicoDTO> servicosDTO = new List<ServicoDTO>();
        public ServicoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<ServicoDTO> GetServicos()
        {
            var servicos = _context.Servicos.ToList();

            foreach (var servico in servicos)
            {
                var servicoDTO = new ServicoDTO();
                servicoDTO.ServicoId = servico.Id;
                servicoDTO.Nome = servico.Nome;
                servicoDTO.Preco = servico.Preco;
                servicoDTO.Descricao = servico.Descricao;
                servicoDTO.ImagemUrl = servico.ImagemUrl;
                this.servicosDTO.Add(servicoDTO);
            }

            return this.servicosDTO.ToList();
        }

        public ServicoDTO GetServicoById(int id)
        {
            var servico = _context.Servicos.Find(id);
            var servicoDTO = new ServicoDTO();
            servicoDTO.ServicoId = servico.Id;
            servicoDTO.Nome = servico.Nome;
            servicoDTO.Preco = servico.Preco;
            servicoDTO.Descricao = servico.Descricao;
            servicoDTO.ImagemUrl = servico.ImagemUrl;

            return servicoDTO;
        }
    }
}
