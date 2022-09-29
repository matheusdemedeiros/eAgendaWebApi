using AutoMapper;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Webapi.ViewModels.ModuloDespesa;
using System.Collections.Generic;

namespace eAgenda.Webapi.Config.AutoMapperConfig.Resolvers
{
    public class CategoriaResolver : IValueResolver<FormsDespesaViewModel, Despesa, List<Categoria>>
    {
        private readonly IRepositorioCategoria repositorioCategoria;

        public CategoriaResolver(IRepositorioCategoria repositorioCategoria)
        {
            this.repositorioCategoria = repositorioCategoria;
        }

        public List<Categoria> Resolve(FormsDespesaViewModel source,
            Despesa destination, List<Categoria> destMember,
            ResolutionContext context)
        {
            var categoriasEncontradas = new List<Categoria>();

            foreach (var item in source.CategoriasId)
            {
                var categoria = repositorioCategoria.SelecionarPorId(item);

                destination.AtribuirCategoria(categoria);
            }

            return categoriasEncontradas;
        }
    }
}
