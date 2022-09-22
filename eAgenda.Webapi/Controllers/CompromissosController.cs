using eAgenda.Aplicacao.ModuloCompromisso;
using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Infra.Configs;
using eAgenda.Infra.Orm;
using eAgenda.Infra.Orm.ModuloCompromisso;
using eAgenda.Webapi.ViewModels.ModuloCompromisso;
using eAgenda.Webapi.ViewModels.ModuloContato;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace eAgenda.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompromissosController : ControllerBase
    {
        private readonly ServicoCompromisso servicoCompromisso;

        public CompromissosController()
        {
            var config = new ConfiguracaoAplicacaoeAgenda();
            var eAgendaDbContext = new eAgendaDbContext(config.ConnectionStrings);
            var repositorioCompromisso = new RepositorioCompromissoOrm(eAgendaDbContext);
            servicoCompromisso = new ServicoCompromisso(repositorioCompromisso, eAgendaDbContext);
        }

        [HttpGet]
        public List<ListarCompromissoViewModel> SelecionarTodos()
        {
            var compromissoResult = servicoCompromisso.SelecionarTodos();

            if (compromissoResult.IsSuccess)
            {
                var compromissosGravados = compromissoResult.Value;

                var listagemCompromissos = new List<ListarCompromissoViewModel>();

                foreach (var item in compromissosGravados)
                {
                    VisualizarContatoViewModel contato = null;
                    if (item.Contato != null)
                    {
                        contato = new VisualizarContatoViewModel
                        {
                            Id = item.Contato.Id,
                            Nome = item.Contato.Nome,
                            Email = item.Contato.Email,
                            Telefone = item.Contato.Telefone,
                            Empresa = item.Contato.Empresa,
                            Cargo = item.Contato.Cargo
                        };
                    }
                    listagemCompromissos.Add(
                    new ListarCompromissoViewModel
                    {
                        Id = item.Id,
                        Assunto = item.Assunto,
                        Data = item.Data,
                        HoraInicio = item.HoraInicio,
                        Contato = contato
                    });
                }

                return listagemCompromissos;
            }

            return null;
        }

        [HttpGet("visualizar-completo/{id:guid}")]
        public VisualizarCompromissoViewModel SelecionarPorId(Guid id)
        {
            var compromissoResult = servicoCompromisso.SelecionarPorId(id);

            if (compromissoResult.IsSuccess)
            {
                var compromisso = compromissoResult.Value;

                VisualizarContatoViewModel contato = null;
                if (compromisso.Contato != null)
                {
                    contato = new VisualizarContatoViewModel
                    {
                        Id = compromisso.Contato.Id,
                        Nome = compromisso.Contato.Nome,
                        Email = compromisso.Contato.Email,
                        Telefone = compromisso.Contato.Telefone,
                        Empresa = compromisso.Contato.Empresa,
                        Cargo = compromisso.Contato.Cargo
                    };
                }
                var compromissoVM = new VisualizarCompromissoViewModel
                {
                    Id = compromisso.Id,
                    Assunto = compromisso.Assunto,
                    Data = compromisso.Data,
                    HoraInicio = compromisso.HoraInicio,
                    HoraTermino = compromisso.HoraTermino,
                    Contato = contato,
                    TipoLocalizacao = compromisso.TipoLocal == TipoLocalizacaoCompromissoEnum.Presencial ? "Presencial" : "Remoto",
                    Link = compromisso.Link,
                    Local = compromisso.Local
                };

                return compromissoVM;
            }

            return null;
        }

        [HttpPost]
        public FormsCompromissoViewModel Inserir(FormsCompromissoViewModel compromissoVM)
        {
            var compromisso = new Compromisso();

            compromisso.Data = compromissoVM.Data;
            compromisso.Local = compromissoVM.Local;
            compromisso.Assunto = compromissoVM.Assunto;
            compromisso.HoraInicio = compromissoVM.HoraInicio;
            compromisso.HoraTermino = compromissoVM.HoraTermino;
            compromisso.TipoLocal = compromissoVM.TipoLocal;
            compromisso.Link = compromissoVM.Link;
            compromisso.Local = compromissoVM.Local;
            compromisso.Contato = new Contato();
            compromisso.Contato.Nome = compromissoVM.Contato.Nome;
            compromisso.Contato.Email = compromissoVM.Contato.Email;
            compromisso.Contato.Telefone = compromissoVM.Contato.Telefone;
            compromisso.Contato.Empresa = compromissoVM.Contato.Empresa;
            compromisso.Contato.Cargo = compromissoVM.Contato.Cargo;
            compromisso.ContatoId = compromissoVM.Contato.Id;

            var compromissoResult = servicoCompromisso.Inserir(compromisso);

            if (compromissoResult.IsSuccess)
                return compromissoVM;

            return null;
        }

        [HttpPut("{id:guid}")]
        public FormsCompromissoViewModel Editar(Guid id, FormsCompromissoViewModel compromissoVM)
        {
            var compromissoEditado = servicoCompromisso.SelecionarPorId(id).Value;

            compromissoEditado.Data = compromissoVM.Data;
            compromissoEditado.Local = compromissoVM.Local;
            compromissoEditado.Assunto = compromissoVM.Assunto;
            compromissoEditado.HoraInicio = compromissoVM.HoraInicio;
            compromissoEditado.HoraTermino = compromissoVM.HoraTermino;
            compromissoEditado.TipoLocal = compromissoVM.TipoLocal;
            compromissoEditado.Link = compromissoVM.Link;
            compromissoEditado.Local = compromissoVM.Local;
            compromissoEditado.Contato = new Contato();
            compromissoEditado.Contato.Nome = compromissoVM.Contato.Nome;
            compromissoEditado.Contato.Email = compromissoVM.Contato.Email;
            compromissoEditado.Contato.Telefone = compromissoVM.Contato.Telefone;
            compromissoEditado.Contato.Empresa = compromissoVM.Contato.Empresa;
            compromissoEditado.Contato.Cargo = compromissoVM.Contato.Cargo;
            compromissoEditado.ContatoId = compromissoVM.Contato.Id;

            var compromissoResult = servicoCompromisso.Editar(compromissoEditado);

            if (compromissoResult.IsSuccess)
                return compromissoVM;

            return null;
        }

        [HttpDelete("{id:guid}")]
        public void Excluir(Guid id)
        {
            servicoCompromisso.Excluir(id);
        }
    }
}