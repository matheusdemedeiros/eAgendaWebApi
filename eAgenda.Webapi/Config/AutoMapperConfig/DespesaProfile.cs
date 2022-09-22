using AutoMapper;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Webapi.ViewModels.ModuloDespesa;

namespace eAgenda.Webapi.Config.AutoMapperConfig
{
    public class DespesaProfile : Profile
    {
        public DespesaProfile()
        {
            ConverterViewModelParaEntidade();

            ConverterEntidadeParaViewModel();

        }

        //Corrigir o erro de inserção duplicada na categoria
        private void ConverterEntidadeParaViewModel()
        {
            CreateMap<InserirDespesaViewModel, Despesa>()
                        .ForMember(destino => destino.Categorias, opt => opt.Ignore())
                        .AfterMap((viewModel, despesa) =>
                        {
                            foreach (var item in viewModel.Categorias)
                            {
                                var categoria = new Categoria();

                                categoria.Titulo = item.Titulo;

                                despesa.AtribuirCategoria(categoria);
                            }
                        });

            CreateMap<EditarDespesaViewModel, Despesa>()
            .ForMember(destino => destino.Categorias, opt => opt.Ignore())
            .AfterMap((viewModel, despesa) =>
            {
                foreach (var item in viewModel.Categorias)
                {
                    if (item.Status == StatusCategoriaEnum.Adicionada)
                    {
                        var categoria = new Categoria();

                        categoria.Titulo = item.Titulo;

                        despesa.AtribuirCategoria(categoria);
                    }
                    else if (item.Status == StatusCategoriaEnum.Removida)
                    {
                        despesa.RemoverCategoria(item.Id);
                    }
                }
            });
        }

        private void ConverterViewModelParaEntidade()
        {
            CreateMap<Categoria, VisualizarCategoriaViewModel>();
            CreateMap<Despesa, ListarDespesaViewModel>();
            CreateMap<Despesa, VisualizarDespesaViewModel>();
        }
    }
}
