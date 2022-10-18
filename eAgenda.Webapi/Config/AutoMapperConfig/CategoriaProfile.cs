using AutoMapper;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Webapi.Config.AutoMapperConfig.Resolvers;
using eAgenda.Webapi.ViewModels.ModuloCategoria;

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

            CreateMap<Categoria, FormsCategoriaViewModel>();
        }

        private void ConverterViewModelParaEntidade()
        {
            CreateMap<ListarCategoriaViewModel, Categoria>();

            CreateMap<VisualizarCategoriaViewModel, Categoria>();

            CreateMap<FormsCategoriaViewModel, Categoria>()
                .ForMember(destino => destino.Id, opt => opt.Ignore())
                .ForMember(destino => destino.UsuarioId, opt => opt.MapFrom<UsuarioResolver>());
        }
    }
}
