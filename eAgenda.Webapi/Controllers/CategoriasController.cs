using AutoMapper;
using eAgenda.Aplicacao.ModuloDespesa;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Webapi.ViewModels.ModuloCategoria;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace eAgenda.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriasController : eAgendaControllerBase
    {
        private readonly ServicoCategoria servicoCategoria;
        private readonly IMapper mapeadorCategorias;

        public CategoriasController(ServicoCategoria servicoCategoria, IMapper mapeadorCategorias)
        {
            this.servicoCategoria = servicoCategoria;
            this.mapeadorCategorias = mapeadorCategorias;
        }

        [HttpGet]
        public ActionResult<List<ListarCategoriaViewModel>> SelecionarTodos()
        {
            var categoriaResult = servicoCategoria.SelecionarTodos(UsuarioLogado.Id);

            if (categoriaResult.IsFailed)
                return InternalError(categoriaResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorCategorias.Map<List<ListarCategoriaViewModel>>(categoriaResult.Value)
            });
        }


        [HttpGet("visualizar-completa/{id:guid}")]
        public ActionResult<VisualizarCategoriaViewModel> SelecionarPorId(Guid id)
        {
            var categoriaResult = servicoCategoria.SelecionarPorId(id);

            if (categoriaResult.IsFailed && RegistroNaoEncontrado(categoriaResult, "não encontrada"))
                return NotFound();

            if (categoriaResult.IsFailed)
                return InternalError(categoriaResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorCategorias.Map<VisualizarCategoriaViewModel>(categoriaResult.Value)
            });
        }

        [HttpPost]
        public ActionResult<FormsCategoriaViewModel> Inserir(FormsCategoriaViewModel categoriaVM)
        {
            var categoria = mapeadorCategorias.Map<Categoria>(categoriaVM);

            categoria.UsuarioId = UsuarioLogado.Id;

            var categoriaResult = servicoCategoria.Inserir(categoria);

            if (categoriaResult.IsFailed)
                return InternalError(categoriaResult);

            return Ok(new
            {
                sucesso = true,
                dados = categoriaVM
            });
        }

        [HttpPut("{id:guid}")]
        public ActionResult<FormsCategoriaViewModel> Editar(Guid id, FormsCategoriaViewModel categoriaVM)
        {
            var categoriaResult = servicoCategoria.SelecionarPorId(id);

            if (categoriaResult.IsFailed && RegistroNaoEncontrado(categoriaResult, "não encontrada"))
                return NotFound();

            var categoria = mapeadorCategorias.Map(categoriaVM, categoriaResult.Value);

            categoriaResult = servicoCategoria.Editar(categoria);

            if (categoriaResult.IsFailed)
                return InternalError(categoriaResult);

            return Ok(new
            {
                sucesso = true,
                dados = categoriaVM
            });
        }

        [HttpDelete("{id:guid}")]
        public ActionResult Excluir(Guid id)
        {
            var categoriaResult = servicoCategoria.Excluir(id);

            if (categoriaResult.IsFailed && RegistroNaoEncontrado<Categoria>(categoriaResult, "não encontrado"))
                return NotFound();

            if (categoriaResult.IsFailed)
                return InternalError<Categoria>(categoriaResult);

            return NoContent();
        }
    }
}