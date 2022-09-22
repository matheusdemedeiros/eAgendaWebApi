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
            CreateMap<FormsContatoViewModel, Contato>();
        }

        private void ConverterViewModelParaEntidade()
        {
            CreateMap<Contato, ListarContatoViewModel>();

            CreateMap<Contato, VisualizarContatoViewModel>();
        }
    }
}
