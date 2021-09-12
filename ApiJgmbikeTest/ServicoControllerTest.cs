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
    public class ServicoControllerTest
    {
        private readonly IServicoAPI _servicoApi;

        public ServicoControllerTest()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json")
                .AddEnvironmentVariables();

            var configuration = builder.Build();

            _servicoApi = RestService.For<IServicoAPI>(configuration["UrlWebAppTestes"]);
        }

        //Success
        [Fact]
        public async Task GetAllServicos()
        {
            var response = await _servicoApi.GetAllAsync();
            response.StatusCode.Should().Be(HttpStatusCode.OK, $"Ocorreu uma falha: Status esperado (OK, 200)");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetServicoByIdSuccess(int id)
        {
            var response = await _servicoApi.GetByIdAsync(id);
            response.StatusCode.Should().Be(HttpStatusCode.OK, $"Ocorreu uma falha no id {id}: Status esperado (OK, 200)");
        }

        //Fail
        [Theory]
        [InlineData(-1)]
        [InlineData(9999999)]
        public async Task GetServicoByIdFail(int id)
        {
            var response = await _servicoApi.GetByIdAsync(id);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest, $"Ocorreu uma falha no id {id}: Status esperado (BadRequest, 400)");
        }
    }
}
