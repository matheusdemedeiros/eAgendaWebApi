using AutoMapper;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Webapi.Config.AutoMapperConfig.Resolvers;
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

            CreateMap<FormsContatoViewModel, Contato>()
                 .ForMember(destino => destino.UsuarioId, opt => opt.MapFrom<UsuarioResolver>())
                 .ForMember(destino => destino.Id, opt => opt.Ignore());
        }
    }
}
