using AutoMapper;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Webapi.ViewModels.ModuloContato;

namespace eAgenda.Webapi.Config.AutoMapperConfig
{
    public class ContatoProfile : Profile
    {
        public ContatoProfile()
        {
            ConverterViewModelParaEntidade();

            ConverterEntidadeParaViewModel();
        }

        private void ConverterEntidadeParaViewModel()
        {
            CreateMap<Contato, FormsContatoViewModel>();

            CreateMap<Contato, ListarContatoViewModel>();
        }

        private void ConverterViewModelParaEntidade()
        {
            CreateMap<ListarContatoViewModel, Contato>();

            CreateMap<VisualizarContatoViewModel, Contato>();

            CreateMap<FormsContatoViewModel, Contato>();
        }
    }
}
