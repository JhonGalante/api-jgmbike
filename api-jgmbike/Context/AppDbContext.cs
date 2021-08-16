using api_jgmbike.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_jgmbike.Context
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<CategoriaProduto> CategoriasProdutos { get; set; }
    }
}
