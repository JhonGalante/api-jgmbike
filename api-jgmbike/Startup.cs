using api_jgmbike.Context;
using api_jgmbike.Repository;
using api_jgmbike.Repository.CategoriaProdutosRepository;
using api_jgmbike.Repository.ProdutoRepository;
using api_jgmbike.Repository.ServicoRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace api_jgmbike
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options =>
            {
                options.AddPolicy("PoliticaJGMBike",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                                .AllowAnyMethod();
                    });

                options.AddPolicy("PoliticaAPIRequest",
                    builder =>
                    {
                        builder.WithOrigins("https://apirequest.io")
                                .WithMethods("POST");
                    });
            });

            services.AddControllers();

            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IServicoRepository, ServicoRepository>();
            services.AddScoped<ICategoriaProdutosRepository, CategoriaProdutosRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "WebAPI JGM Bike", 
                    Version = "v1",
                    Description = "WebAPI da aplicação da aplicação de catálogo da JGM Bike",
                    Contact = new OpenApiContact
                    {
                        Name = "Jhonata Galante",
                        Email = "jhonata.galante@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/jhonata-galante-495947b0/")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "api_jgmbike v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
