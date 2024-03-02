using ControladorEstoqueWebAPI.Services;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Context;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Localization;

namespace ControladorEstoqueWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ControladorEstoqueContext>();
            builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
            builder.Services.AddScoped<ILoteRepository, LoteRepository>();

            // Add services to the container.
            builder.Services.AddScoped<ILoteService, LoteService>();
            builder.Services.AddScoped<IProdutoService, ProdutoService>();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<RequestLocalizationOptions>(options => options.DefaultRequestCulture = new RequestCulture("pt-BR"));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
