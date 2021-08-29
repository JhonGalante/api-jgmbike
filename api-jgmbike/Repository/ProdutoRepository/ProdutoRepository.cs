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
                produtoDTO.ProdutoId = produto.Id;
                produtoDTO.Nome = produto.Nome;
                produtoDTO.Preco = produto.Preco;
                produtoDTO.Descricao = produto.Descricao;
                produtoDTO.ImagemUrl = produto.ImagemUrl;
                produtoDTO.CategoriaId = produto.CategoriaId;
                produtoDTO.CategoriaNome = produto.Categoria.Nome;
                produtoDTO.Disponivel = (produto.Estoque > 0) ? true : false;
                this.produtosDTO.Add(produtoDTO);
            }

            return this.produtosDTO.OrderByDescending(p => p.Disponivel).ToList();
        }
    }
}
