using AutoMapper;
using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Webapi.ViewModels.ModuloCompromisso;
using eAgenda.Webapi.ViewModels.ModuloContato;

namespace eAgenda.Webapi.Config.AutoMapperConfig
{
    public class CompromissoProfile : Profile
    {

        public CompromissoProfile()
        {
            ConverterViewModelParaEntidade();

            ConverterEntidadeParaViewModel();
        }

        private void ConverterViewModelParaEntidade()
        {
            CreateMap<Contato, VisualizarContatoViewModel>();

            CreateMap<Compromisso, ListarCompromissoViewModel>();

            CreateMap<Compromisso, VisualizarCompromissoViewModel>()
            .ForMember(destino => destino.TipoLocalizacao,
            opt => opt.MapFrom(origem => origem.TipoLocal.GetDescription()));
        }

        private void ConverterEntidadeParaViewModel()
        {
            CreateMap<FormsCompromissoViewModel, Compromisso>();
        }
    }
}
