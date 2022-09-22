using AutoMapper;
using eAgenda.Aplicacao.ModuloCompromisso;
using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.Webapi.ViewModels.ModuloCompromisso;
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
        private readonly IMapper mapeadorCompromissos;

        public CompromissosController(ServicoCompromisso servicoCompromisso, IMapper mapeadorCompromissos)
        {
            this.servicoCompromisso = servicoCompromisso;
            this.mapeadorCompromissos = mapeadorCompromissos;
        }

        [HttpGet]
        public List<ListarCompromissoViewModel> SelecionarTodos()
        {
            var compromissoResult = servicoCompromisso.SelecionarTodos();

            if (compromissoResult.IsSuccess)
                return mapeadorCompromissos.Map<List<ListarCompromissoViewModel>>(compromissoResult.Value);

            return null;
        }

        [HttpGet("visualizar-completo/{id:guid}")]
        public VisualizarCompromissoViewModel SelecionarPorId(Guid id)
        {
            var compromissoResult = servicoCompromisso.SelecionarPorId(id);

            if (compromissoResult.IsSuccess)
                return mapeadorCompromissos.Map<VisualizarCompromissoViewModel>(compromissoResult.Value);

            return null;
        }

        [HttpPost]
        public FormsCompromissoViewModel Inserir(FormsCompromissoViewModel compromissoVM)
        {
            var compromisso = mapeadorCompromissos.Map<Compromisso>(compromissoVM);

            var compromissoResult = servicoCompromisso.Inserir(compromisso);

            if (compromissoResult.IsSuccess)
                return compromissoVM;

            return null;
        }

        [HttpPut("{id:guid}")]
        public FormsCompromissoViewModel Editar(Guid id, FormsCompromissoViewModel compromissoVM)
        {
            var compromissoSelecionado = servicoCompromisso.SelecionarPorId(id).Value;

            var compromisso = mapeadorCompromissos.Map(compromissoVM, compromissoSelecionado);
            
            var compromissoResult = servicoCompromisso.Editar(compromisso);

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