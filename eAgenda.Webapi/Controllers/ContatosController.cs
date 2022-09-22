using AutoMapper;
using eAgenda.Aplicacao.ModuloContato;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Infra.Configs;
using eAgenda.Infra.Orm;
using eAgenda.Infra.Orm.ModuloContato;
using eAgenda.Webapi.ViewModels.ModuloContato;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace eAgenda.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatosController : ControllerBase
    {
        private readonly ServicoContato servicoContato;
        private IMapper mapeadorContatos;

        public ContatosController()
        {
            var config = new ConfiguracaoAplicacaoeAgenda();
            var eAgendaDbContext = new eAgendaDbContext(config.ConnectionStrings);
            var repositorioContato = new RepositorioContatoOrm(eAgendaDbContext);
            servicoContato = new ServicoContato(repositorioContato, eAgendaDbContext);

            var autoMapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Contato, ListarContatoViewModel>();

                config.CreateMap<Contato, VisualizarContatoViewModel>();

                config.CreateMap<FormsContatoViewModel, Contato>();

            });

            mapeadorContatos = autoMapperConfig.CreateMapper();
        }

        [HttpGet]
        public List<ListarContatoViewModel> SelecionarTodos()
        {
            var contatoResult = servicoContato.SelecionarTodos();

            if (contatoResult.IsSuccess)
            {
                return mapeadorContatos.Map<List<ListarContatoViewModel>>(contatoResult.Value);

            }

            return null;
        }

        [HttpGet("visualizar-completo/{id:guid}")]
        public VisualizarContatoViewModel SelecionarPorId(Guid id)
        {
            var contatoResult = servicoContato.SelecionarPorId(id);

            if (contatoResult.IsSuccess)
            {
                return mapeadorContatos.Map<VisualizarContatoViewModel>(contatoResult.Value);
            }

            return null;
        }

        [HttpPost]
        public FormsContatoViewModel Inserir(FormsContatoViewModel contatoVM)
        {
            var contato = mapeadorContatos.Map<Contato>(contatoVM);

            var contatoResult = servicoContato.Inserir(contato);

            if (contatoResult.IsSuccess)
                return contatoVM;

            return null;
        }

        [HttpPut("{id:guid}")]
        public FormsContatoViewModel Editar(Guid id, FormsContatoViewModel contatoVM)
        {
            var contatoSelecionado = servicoContato.SelecionarPorId(id).Value;

            var contato = mapeadorContatos.Map(contatoVM, contatoSelecionado);

            var contatoResult = servicoContato.Editar(contato);

            if (contatoResult.IsSuccess)
                return contatoVM;

            return null;
        }

        [HttpDelete("{id:guid}")]
        public void Excluir(Guid id)
        {
            servicoContato.Excluir(id);
        }
    }
}
