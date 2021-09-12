﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiJgmbikeTest.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public int Estoque { get; set; }
        public string ImagemUrl { get; set; }
        public string CategoriaNome { get; set; }
        public int CategoriaId { get; set; }
    }
}
