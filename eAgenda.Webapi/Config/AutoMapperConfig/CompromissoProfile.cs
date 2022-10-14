using AutoMapper;
using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Webapi.Config.AutoMapperConfig.Resolvers;
using eAgenda.Webapi.ViewModels.ModuloCompromisso;
using eAgenda.Webapi.ViewModels.ModuloContato;
using System;

namespace eAgenda.Webapi.Config.AutoMapperConfig
{
    public class CompromissoProfile : Profile
    {

        public CompromissoProfile()
        {
            ConverterViewModelParaEntidade();

            ConverterEntidadeParaViewModel();
        }

        private void ConverterEntidadeParaViewModel()
        {
            CreateMap<Contato, VisualizarContatoViewModel>();

            CreateMap<Compromisso, ListarCompromissoViewModel>();

            CreateMap<Compromisso, VisualizarCompromissoViewModel>()
            .ForMember(destino => destino.TipoLocalizacao,
            opt => opt.MapFrom(origem => origem.TipoLocal.GetDescription()));
        }

        private void ConverterViewModelParaEntidade()
        {
            CreateMap<FormsCompromissoViewModel, Compromisso>()
            .ForMember(destino => destino.UsuarioId, opt => opt.MapFrom<UsuarioResolver>())
            .ForMember(destino => destino.Id, opt => opt.Ignore())
            .ForMember(destino => destino.ContatoId, opt => opt.Ignore())
            .AfterMap((viewModel, compromisso) =>
            {
                if (!viewModel.ContatoId.Equals(Guid.Empty))
                    compromisso.ContatoId = viewModel.ContatoId;
            });
        }
    }
}
