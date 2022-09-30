using AutoMapper;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Webapi.Config.AutoMapperConfig.Resolvers;
using eAgenda.Webapi.ViewModels.ModuloCategoria;
using eAgenda.Webapi.ViewModels.ModuloDespesa;

namespace eAgenda.Webapi.Config.AutoMapperConfig
{
    public class DespesaProfile : Profile
    {
        public IRepositorioCategoria RepositorioCategoria;


        public DespesaProfile()
        {
            ConverterViewModelParaEntidade();

            ConverterEntidadeParaViewModel();
        }


        public DespesaProfile(IRepositorioCategoria repositorioCategoria)
        {
            RepositorioCategoria = repositorioCategoria;

            ConverterViewModelParaEntidade();

            ConverterEntidadeParaViewModel();
        }

        private void ConverterViewModelParaEntidade()
        {
            CreateMap<InserirDespesaViewModel, Despesa>()
               .ForMember(destino => destino.UsuarioId, opt => opt.MapFrom<UsuarioResolver>())
                .AfterMap<ConfigurarCategoriasMappingAction>();


            CreateMap<EditarDespesaViewModel, Despesa>()
                .AfterMap<ConfigurarCategoriasMappingAction>();
        }

        private void ConverterEntidadeParaViewModel()
        {
            CreateMap<Categoria, VisualizarCategoriaViewModel>();
            CreateMap<Despesa, ListarDespesaViewModel>();
            CreateMap<Despesa, VisualizarDespesaViewModel>();

        }
    }
}
