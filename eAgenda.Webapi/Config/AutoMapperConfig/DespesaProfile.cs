using AutoMapper;
using eAgenda.Dominio.ModuloDespesa;
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

        private void ConverterEntidadeParaViewModel()
        {
            CreateMap<InserirDespesaViewModel, Despesa>()
                        .ForMember(destino => destino.Categorias, opt => opt.Ignore())
                        .AfterMap((viewmodel, entidade ) =>
                        {
                            //foreach (var item in viewmodel.CategoriasId)
                            //{
                            //    entidade.AtribuirCategoria(RepositorioCategoria.SelecionarPorId(item));
                            //}
                        });
                        

            CreateMap<EditarDespesaViewModel, Despesa>()
            .ForMember(destino => destino.Categorias, opt => opt.Ignore());
        }

        private void ConverterViewModelParaEntidade()
        {
            CreateMap<Categoria, VisualizarCategoriaViewModel>();
            CreateMap<Despesa, ListarDespesaViewModel>();
            CreateMap<Despesa, VisualizarDespesaViewModel>();

        }
    }
}
