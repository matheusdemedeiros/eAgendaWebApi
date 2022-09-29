using AutoMapper;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Webapi.ViewModels.ModuloCategoria;
using System;

namespace eAgenda.Webapi.Config.AutoMapperConfig
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            ConverterViewModelParaEntidade();

            ConverterEntidadeParaViewModel();
        }

        private void ConverterEntidadeParaViewModel()
        {
            CreateMap<Categoria, ListarCategoriaViewModel>();
            
            CreateMap<Categoria, VisualizarCategoriaViewModel>();
        }

        private void ConverterViewModelParaEntidade()
        {
            CreateMap<ListarCategoriaViewModel, Categoria>();
            
            CreateMap<VisualizarCategoriaViewModel, Categoria>();
            
            CreateMap<FormsCategoriaViewModel, Categoria>();
        }
    }
}
