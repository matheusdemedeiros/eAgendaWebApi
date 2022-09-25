using Microsoft.AspNetCore.Mvc;
using eAgenda.Aplicacao.ModuloDespesa;
using System.Collections.Generic;
using eAgenda.Webapi.ViewModels.ModuloDespesa;
using System;
using eAgenda.Dominio.ModuloDespesa;
using AutoMapper;
using System.Linq;

namespace eAgenda.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaController : eAgendaControllerBase
    {
        private readonly ServicoDespesa servicoDespesa;
        private readonly IMapper mapeadorDespesa;
        private readonly IRepositorioCategoria repositorioCategoria;

        public DespesaController(ServicoDespesa servicoDespesa, IMapper mapeadorDespesa, IRepositorioCategoria repositorioCategoria)
        {
            this.servicoDespesa = servicoDespesa;
            this.mapeadorDespesa = mapeadorDespesa;
            this.repositorioCategoria = repositorioCategoria;
        }

        [HttpGet]
        public ActionResult<List<ListarDespesaViewModel>> SelecionarTodos()
        {
            var despesaResult = servicoDespesa.SelecionarTodos();

            if (despesaResult.IsFailed)
                return InternalError(despesaResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorDespesa.Map<List<ListarDespesaViewModel>>(despesaResult.Value)
            });
        }

        [HttpGet("visualizar-completo/{id:guid}")]
        public ActionResult<VisualizarDespesaViewModel> SelecionarPorId(Guid id)
        {
            var despesaResult = servicoDespesa.SelecionarPorId(id);

            if (despesaResult.IsFailed && RegistroNaoEncontrado(despesaResult, "não encontrada"))
                return NotFound();

            if (despesaResult.IsFailed)
                return InternalError(despesaResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorDespesa.Map<VisualizarDespesaViewModel>(despesaResult.Value)
            });
        }

        [HttpPost]
        public ActionResult<FormsDespesaViewModel> Inserir(InserirDespesaViewModel despesaVM)
        {
            var listaErros = ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage);

            if (listaErros.Any())
            {
                return BadRequest(new
                {
                    sucesso = false,
                    erros = listaErros.ToList()
                });
            }

            var despesa = mapeadorDespesa.Map<Despesa>(despesaVM);

            foreach (var item in despesaVM.CategoriasId)
            {
                despesa.AtribuirCategoria(repositorioCategoria.SelecionarPorId(item));
            }

            var despesaResult = servicoDespesa.Inserir(despesa);

            if (despesaResult.IsFailed)
                return InternalError(despesaResult);

            return Ok(new
            {
                sucesso = true,
                dados = despesaVM
            });


            //despesa.Descricao = despesaVM.Descricao;
            //despesa.Valor = despesaVM.Valor;
            //despesa.Data = despesaVM.Data;
            //despesa.FormaPagamento = despesaVM.FormaPagamento;

            //foreach (var item in despesaVM.Categorias)
            //{
            //    var categoria = new Categoria();

            //    categoria.Titulo = item.Titulo;

            //    despesa.AtribuirCategoria(categoria);
            //}
        }

        [HttpPut("{id:guid}")]
        public ActionResult<FormsDespesaViewModel> Editar(Guid id, EditarDespesaViewModel despesaVM)
        {
            var listaErros = ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage);

            if (listaErros.Any())
            {
                return BadRequest(new
                {
                    sucesso = false,
                    erros = listaErros.ToList()
                });
            }

            var despesaResult = servicoDespesa.SelecionarPorId(id);

            if (despesaResult.IsFailed && RegistroNaoEncontrado(despesaResult, "não encontrada"))
                return NotFound();

            var despesa = mapeadorDespesa.Map(despesaVM, despesaResult.Value);

            despesaResult = servicoDespesa.Editar(despesa);

            if (despesaResult.IsFailed)
                return InternalError(despesaResult);

            return Ok(new
            {
                sucesso = true,
                dados = despesaVM
            });

            //despesaEditada.Descricao = despesaVM.Descricao;
            //despesaEditada.Valor = despesaVM.Valor;
            //despesaEditada.Data = despesaVM.Data;
            //despesaEditada.FormaPagamento = despesaVM.FormaPagamento;

            //foreach (var item in despesaVM.Categorias)
            //{
            //    if (item.Status == StatusCategoriaEnum.Adicionada)
            //    {
            //        var categoria = new Categoria();

            //        categoria.Titulo = item.Titulo;

            //        despesaEditada.AtribuirCategoria(categoria);
            //    }
            //    else if (item.Status == StatusCategoriaEnum.Removida)
            //    {
            //        despesaEditada.RemoverCategoria(item.Id);
            //    }
            //}

        }

        [HttpDelete("{id:guid}")]
        public ActionResult Excluir(Guid id)
        {
            var despesaResult = servicoDespesa.Excluir(id);

            if (despesaResult.IsFailed && RegistroNaoEncontrado<Despesa>(despesaResult,"não encontrada"))
                return NotFound();

            if (despesaResult.IsFailed)
                return InternalError<Despesa>(despesaResult);
            
            return NoContent();
        }
    }
}