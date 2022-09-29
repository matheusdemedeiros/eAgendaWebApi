using AutoMapperBuilder.Extensions.DependencyInjection;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Webapi.Config;
using eAgenda.Webapi.Config.AutoMapperConfig;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace eAgenda.Webapi
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
            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.SuppressModelStateInvalidFilter = true;
            });

            services.AddAutoMapper(typeof(Startup));

            //services.AddAutoMapperBuilder(builder =>
            //{
            //    builder.Profiles.Add(new DespesaProfile(services.BuildServiceProvider()
            //        .GetRequiredService<IRepositorioCategoria>()));

            //});

            //services.AddAutoMapper(config =>
            //{
            //    config.AddProfile<TarefaProfile>();
            //    config.AddProfile<ContatoProfile>();
            //    config.AddProfile<CompromissoProfile>();
            //    config.AddProfile<UsuarioProfile>();
            //});

            services.ConfigurarInjecaoDependencia();
            services.ConfigurarAutenticacao();
            services.ConfigurarFiltros();
            services.ConfigurarSwagger();
            services.ConfigurarJwt();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "eAgenda.Webapi v1"));
            }

            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}