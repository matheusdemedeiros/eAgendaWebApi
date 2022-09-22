using eAgenda.Infra.Configs;
using Microsoft.AspNetCore.Mvc;
using eAgenda.Infra.Orm;
using eAgenda.Aplicacao.ModuloDespesa;
using eAgenda.Infra.Orm.ModuloDespesa;
using System.Collections.Generic;
using eAgenda.Webapi.ViewModels.ModuloDespesa;
using System;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Dominio.Compartilhado;
using AutoMapper;

namespace eAgenda.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaController : ControllerBase
    {
        private readonly ServicoDespesa servicoDespesa;
        private readonly IMapper mapeadorDespesa;

        public DespesaController(ServicoDespesa servicoDespesa, IMapper mapeadorDespesa)
        {
            this.servicoDespesa = servicoDespesa;
            this.mapeadorDespesa = mapeadorDespesa;
        }

        [HttpGet]
        public List<ListarDespesaViewModel> SelecionarTodos()
        {
            var despesaResult = servicoDespesa.SelecionarTodos();

            if (despesaResult.IsSuccess)
                return mapeadorDespesa.Map<List<ListarDespesaViewModel>>(despesaResult.Value);

            return null;
        }

        [HttpGet("visualizar-completo/{id:guid}")]
        public VisualizarDespesaViewModel SelecionarPorId(Guid id)
        {
            var despesaResult = servicoDespesa.SelecionarPorId(id);

            if (despesaResult.IsSuccess)
                return mapeadorDespesa.Map<VisualizarDespesaViewModel>(despesaResult.Value);

            return null;
        }

        [HttpPost]
        public FormsDespesaViewModel Inserir(InserirDespesaViewModel despesaVM)
        {
            var despesa = mapeadorDespesa.Map<Despesa>(despesaVM);

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

            var despesaResult = servicoDespesa.Inserir(despesa);

            if (despesaResult.IsSuccess)
                return despesaVM;

            return null;
        }

        [HttpPut("{id:guid}")]
        public FormsDespesaViewModel Editar(Guid id, EditarDespesaViewModel despesaVM)
        {
            var despesaSelecionada = servicoDespesa.SelecionarPorId(id).Value;

            var despesa = mapeadorDespesa.Map(despesaVM, despesaSelecionada);

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

            var despesaResult = servicoDespesa.Editar(despesa);

            if (despesaResult.IsSuccess)
                return despesaVM;

            return null;
        }

        [HttpDelete("{id:guid}")]
        public void Excluir(Guid id)
        {
            servicoDespesa.Excluir(id);
        }
    }
}