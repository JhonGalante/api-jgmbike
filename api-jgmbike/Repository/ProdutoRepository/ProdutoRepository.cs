using api_jgmbike.Context;
using api_jgmbike.DTOs;
using api_jgmbike.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_jgmbike.Repository.ProdutoRepository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        private List<ProdutoDTO> produtosDTO = new List<ProdutoDTO>();
        private readonly AppDbContext _context;
        public ProdutoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<ProdutoDTO> GetProdutosCategorias()
        {
            var produtos = _context.Produtos.Include(p => p.Categoria).ToList();
            
            foreach (var produto in produtos)
            {
                var produtoDTO = new ProdutoDTO();
                produtoDTO.CategoriaId = produto.Id;
                produtoDTO.Nome = produto.Nome;
                produtoDTO.Preco = produto.Preco;
                produtoDTO.Estoque = produto.Estoque;
                produtoDTO.Descricao = produto.Descricao;
                produtoDTO.ImagemUrl = produto.ImagemUrl;
                produtoDTO.CategoriaId = produto.CategoriaId;
                produtoDTO.CategoriaNome = produto.Categoria.Nome;
                this.produtosDTO.Add(produtoDTO);
            }

            return this.produtosDTO;
        }
    }
}
