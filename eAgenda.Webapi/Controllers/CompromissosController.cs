using AutoMapper;
using eAgenda.Aplicacao.ModuloCompromisso;
using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.Webapi.ViewModels.ModuloCompromisso;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace eAgenda.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompromissosController : eAgendaControllerBase
    {
        private readonly ServicoCompromisso servicoCompromisso;
        private readonly IMapper mapeadorCompromissos;

        public CompromissosController(ServicoCompromisso servicoCompromisso, IMapper mapeadorCompromissos)
        {
            this.servicoCompromisso = servicoCompromisso;
            this.mapeadorCompromissos = mapeadorCompromissos;
        }

        [HttpGet]
        public ActionResult<List<ListarCompromissoViewModel>> SelecionarTodos()
        {
            var compromissoResult = servicoCompromisso.SelecionarTodos(UsuarioLogado.Id);

            if (compromissoResult.IsFailed)
                return InternalError(compromissoResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorCompromissos.Map<List<ListarCompromissoViewModel>>(compromissoResult.Value)
            });
        }

        [HttpGet("visualizar-completo/{id:guid}")]
        public ActionResult<VisualizarCompromissoViewModel> SelecionarPorId(Guid id)
        {
            var compromissoResult = servicoCompromisso.SelecionarPorId(id);

            if (compromissoResult.IsFailed && RegistroNaoEncontrado(compromissoResult, "não encontrado"))
                return NotFound();

            if (compromissoResult.IsFailed)
                return InternalError(compromissoResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorCompromissos.Map<VisualizarCompromissoViewModel>(compromissoResult.Value)
            });
        }

        [HttpPost]
        public ActionResult<FormsCompromissoViewModel> Inserir(FormsCompromissoViewModel compromissoVM)
        {
            var compromisso = mapeadorCompromissos.Map<Compromisso>(compromissoVM);

            compromisso.UsuarioId = UsuarioLogado.Id;

            var compromissoResult = servicoCompromisso.Inserir(compromisso);

            if (compromissoResult.IsFailed)
                return InternalError(compromissoResult);

            return Ok(new
            {
                sucesso = true,
                dados = compromissoVM
            });
        }

        [HttpPut("{id:guid}")]
        public ActionResult<FormsCompromissoViewModel> Editar(Guid id, FormsCompromissoViewModel compromissoVM)
        {
            var compromissoResult = servicoCompromisso.SelecionarPorId(id);

            if (compromissoResult.IsFailed && RegistroNaoEncontrado(compromissoResult, "não encontrado"))
                return NotFound();

            var compromisso = mapeadorCompromissos.Map(compromissoVM, compromissoResult.Value);

            compromissoResult = servicoCompromisso.Editar(compromisso);

            if (compromissoResult.IsFailed)
                return InternalError(compromissoResult);

            return Ok(new
            {
                sucesso = true,
                dados = compromissoVM
            });
        }

        [HttpDelete("{id:guid}")]
        public ActionResult Excluir(Guid id)
        {
            var compromissoResult = servicoCompromisso.Excluir(id);

            if (compromissoResult.IsFailed && RegistroNaoEncontrado<Compromisso>(compromissoResult, "não encontrado"))
                return NotFound();

            if (compromissoResult.IsFailed)
                return InternalError<Compromisso>(compromissoResult);

            return NoContent();
        }
    }
}
