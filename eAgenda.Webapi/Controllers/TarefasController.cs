﻿using eAgenda.Aplicacao.ModuloTarefa;
using eAgenda.Dominio.ModuloTarefa;
using eAgenda.Infra.Orm.ModuloTarefa;
using eAgenda.Infra.Orm;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using eAgenda.Infra.Configs;
using System;
using eAgenda.Webapi.ViewModels;
using AutoMapper;
using eAgenda.Webapi.Config.AutoMapperConfig;

namespace eAgenda.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private readonly ServicoTarefa servicoTarefa;

        private readonly IMapper mapeadorTarefas;

        public TarefasController(ServicoTarefa servicoTarefa, IMapper mapeadorTarefas)
        {
            this.servicoTarefa = servicoTarefa;
            this.mapeadorTarefas = mapeadorTarefas;
        }

        [HttpGet]
        public List<ListarTarefaViewModel> SelecionarTodos()
        {
            var tarefaResult = servicoTarefa.SelecionarTodos(StatusTarefaEnum.Todos);

            if (tarefaResult.IsSuccess)
                return mapeadorTarefas.Map<List<ListarTarefaViewModel>>(tarefaResult.Value);

            return null;
        }

        [HttpGet("visualizar-completa/{id:guid}")]
        public VisualizarTarefaViewModel SelecionarTarefaCompletaPorId(Guid id)
        {
            var tarefaResult = servicoTarefa.SelecionarPorId(id);

            if (tarefaResult.IsSuccess)
                return mapeadorTarefas.Map<VisualizarTarefaViewModel>(tarefaResult.Value);

            return null;
        }

        [HttpPost]
        public FormsTarefaViewModel Inserir(InserirTarefaViewModel tarefaVM)
        {
            var tarefa = mapeadorTarefas.Map<Tarefa>(tarefaVM);

            var tarefaResult = servicoTarefa.Inserir(tarefa);

            if (tarefaResult.IsSuccess)
                return tarefaVM;

            return null;
        }

        [HttpPut("{id:guid}")]
        public FormsTarefaViewModel Editar(Guid id, EditarTarefaViewModel tarefaVM)
        {
            var tarefaSelecionada = servicoTarefa.SelecionarPorId(id).Value;

            var tarefa = mapeadorTarefas.Map(tarefaVM, tarefaSelecionada);

            var tarefaResult = servicoTarefa.Editar(tarefa);

            if (tarefaResult.IsSuccess)
                return tarefaVM;

            return null;
        }

        [HttpDelete("{id:guid}")]
        public void Excluir(Guid id)
        {
            servicoTarefa.Excluir(id);
        }
    }
}
