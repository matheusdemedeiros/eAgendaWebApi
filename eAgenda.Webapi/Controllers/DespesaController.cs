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

namespace eAgenda.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaController : ControllerBase
    {
        private readonly ServicoDespesa servicoDespesa;

        public DespesaController()
        {
            var config = new ConfiguracaoAplicacaoeAgenda();
            var eAgendaDbContext = new eAgendaDbContext(config.ConnectionStrings);
            var repositorioDespesa = new RepositorioDespesaOrm(eAgendaDbContext);
            servicoDespesa = new ServicoDespesa(repositorioDespesa, eAgendaDbContext);
        }

        [HttpGet]
        public List<ListarDespesaViewModel> SelecionarTodos()
        {
            var despesaResult = servicoDespesa.SelecionarTodos();

            if (despesaResult.IsSuccess)
            {
                var despesasGravadas = despesaResult.Value;

                var listagemDespesas = new List<ListarDespesaViewModel>();

                foreach (var item in despesasGravadas)
                {
                    listagemDespesas.Add(
                        new ListarDespesaViewModel()
                        {
                            Id = item.Id,
                            Descricao = item.Descricao,
                            Valor = item.Valor,
                            Data = item.Data
                        }
                    );
                }
                return listagemDespesas;
            }

            return null;
        }

        [HttpGet("visualizar-completo/{id:guid}")]
        public VisualizarDespesaViewModel SelecionarPorId(Guid id)
        {
            var despesaResult = servicoDespesa.SelecionarPorId(id);

            if (despesaResult.IsSuccess)
            {
                var despesaVM = new VisualizarDespesaViewModel();

                var despesa = despesaResult.Value;

                despesaVM.Id = despesa.Id;
                despesaVM.Descricao = despesa.Descricao;
                despesaVM.Valor = despesa.Valor;
                despesaVM.Data = despesa.Data;
                despesaVM.FormaPagamento = despesa.FormaPagamento.GetDescription();

                foreach (var item in despesa.Categorias)
                {
                    despesaVM.Categorias.Add(
                        new VisualizarCategoriaViewModel { Titulo = item.Titulo });
                }
                return despesaVM;
            }

            return null;
        }

        [HttpPost]
        public FormsDespesaViewModel Inserir(FormsDespesaViewModel despesaVM)
        {
            var despesa = new Despesa();

            despesa.Descricao = despesaVM.Descricao;
            despesa.Valor = despesaVM.Valor;
            despesa.Data = despesaVM.Data;
            despesa.FormaPagamento = despesaVM.FormaPagamento;

            foreach (var item in despesaVM.Categorias)
            {
                var categoria = new Categoria();

                categoria.Titulo = item.Titulo;

                despesa.AtribuirCategoria(categoria);
            }

            var despesaResult = servicoDespesa.Inserir(despesa);

            if (despesaResult.IsSuccess)
                return despesaVM;

            return null;
        }

        [HttpPut("{id:guid}")]
        public FormsDespesaViewModel Editar(Guid id, FormsDespesaViewModel despesaVM)
        {
            var despesaEditada = servicoDespesa.SelecionarPorId(id).Value;

            despesaEditada.Descricao = despesaVM.Descricao;
            despesaEditada.Valor = despesaVM.Valor;
            despesaEditada.Data = despesaVM.Data;
            despesaEditada.FormaPagamento = despesaVM.FormaPagamento;

            foreach (var item in despesaVM.Categorias)
            {
                if (item.Status == StatusCategoriaEnum.Adicionada)
                {
                    var categoria = new Categoria();

                    categoria.Titulo = item.Titulo;

                    despesaEditada.AtribuirCategoria(categoria);
                }
                else if (item.Status == StatusCategoriaEnum.Removida)
                {
                    despesaEditada.RemoverCategoria(item.Id);
                }
            }

            var despesaResult = servicoDespesa.Editar(despesaEditada);

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