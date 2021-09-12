using ApiJgmbikeTest.HttpClients;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Refit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApiJgmbikeTest
{
    public class ProdutoControllerTest
    {
        private readonly IProdutoAPI _produtoApi;

        public ProdutoControllerTest()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json")
                .AddEnvironmentVariables();
            var configuration = builder.Build();

            _produtoApi = RestService.For<IProdutoAPI>(configuration["UrlWebAppTestes"]);
        }

        //Success
        [Fact]
        public async Task GetAllProdutosSuccess()
        {
            var response = await _produtoApi.GetAllAsync();

            response.StatusCode.Should().Be(HttpStatusCode.OK, $"Ocorreu uma falha: Status esperado (OK, 200)");
        }

        [Fact]
        public async Task GetAllProdutosCategoriasSuccess()
        {
            var response = await _produtoApi.GetAllProdutosCategoriasAsync();

            response.StatusCode.Should().Be(HttpStatusCode.OK, $"Ocorreu uma falha: Status esperado (OK, 200)");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        public async Task GetProdutoByIdSuccess(int id)
        {
            var response = await _produtoApi.GetByIdAsync(id);
            response.StatusCode.Should().Be(HttpStatusCode.OK, $"Ocorreu uma falha no id {id}: Status esperado (OK, 200)");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        public async Task GetProdutosByCategoriaIdSuccess(int id)
        {
            var response = await _produtoApi.GetProdutosPorCategoriaAsync(id);
            response.StatusCode.Should().Be(HttpStatusCode.OK, $"Ocorreu uma falha no id {id}: Status esperado (OK, 200)");
        }

        //Fail
        [Theory]
        [InlineData(-1)]
        [InlineData(9999999)]
        public async Task GetProdutoByIdFail(int id)
        {
            var response = await _produtoApi.GetByIdAsync(id);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest, $"Ocorreu uma falha no id {id}: Status esperado (BadRequest, 400)");
        }

        //[Theory]
        //[InlineData(-1)]
        //[InlineData(9999999)]
        //public async Task GetProdutosByCategoriaIdFail(int id)
        //{
        //    var response = await _produtoApi.GetProdutosPorCategoriaAsync(id);
        //    response.StatusCode.Should().Be(HttpStatusCode.BadRequest, $"Ocorreu uma falha no id {id}: Status esperado (BadRequest, 400)");
        //}
    }
}
