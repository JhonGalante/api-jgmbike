using api_jgmbike.Context;
using api_jgmbike.DTOs;
using api_jgmbike.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_jgmbike.Repository.CategoriaProdutosRepository
{
    public class CategoriaProdutosRepository : Repository<CategoriaProduto>, ICategoriaProdutosRepository
    {
        private readonly AppDbContext _context;
        private List<CategoriaProdutoDTO> categoriasDTO = new List<CategoriaProdutoDTO>();
        public CategoriaProdutosRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public CategoriaProdutoDTO GetCategoriaById(int id)
        {
            var categoria = _context.CategoriaProdutos.Find(id);
            var categoriaDTO = new CategoriaProdutoDTO();
            categoriaDTO.CategoriaId = categoria.Id;
            categoriaDTO.Nome = categoria.Nome;
            categoriaDTO.ImagemUrl = categoria.ImagemUrl;

            return categoriaDTO;
        }

        public IEnumerable<CategoriaProdutoDTO> GetCategorias()
        {
            var categorias = _context.CategoriaProdutos.ToList();

            foreach (var categoria in categorias)
            {
                var categoriaDTO = new CategoriaProdutoDTO();
                categoriaDTO.CategoriaId = categoria.Id;
                categoriaDTO.Nome = categoria.Nome;
                categoriaDTO.ImagemUrl = categoria.ImagemUrl;
                this.categoriasDTO.Add(categoriaDTO);
            }

            return this.categoriasDTO.ToList();
        }
    }
}
