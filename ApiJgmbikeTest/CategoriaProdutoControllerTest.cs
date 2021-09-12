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
    public class CategoriaProdutoControllerTest
    {
        private readonly ICategoriaProdutoAPI _categoriaProdutoApi;

        public CategoriaProdutoControllerTest()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json")
                .AddEnvironmentVariables();

            var configuration = builder.Build();

            _categoriaProdutoApi = RestService.For<ICategoriaProdutoAPI>(configuration["UrlWebAppTestes"]);
        }

        //Success
        [Fact]
        public async Task GetAllCategoriasTestSuccess()
        {
            var response = await _categoriaProdutoApi.GetAllAsync();

            response.StatusCode.Should().Be(HttpStatusCode.OK, $"Ocorreu uma falha: Status esperado (200, Ok)");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetByIdCategoriaTestSuccess(int id)
        {
            var response = await _categoriaProdutoApi.GetByIdAsync(id);
            response.StatusCode.Should().Be(HttpStatusCode.OK, $"Ocorreu uma falha: Status esperado (200, Ok)");
        }

        //Fail
        [Theory]
        [InlineData(-1)]
        [InlineData(9999999)]
        public async Task GetByIdCategoriaTestFail(int id)
        {
            var response = await _categoriaProdutoApi.GetByIdAsync(id);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest, $"Ocorreu uma falha: StatusCode para a categoria de id: {id} esperado (400, BadRequest)");
        }
    }
}
