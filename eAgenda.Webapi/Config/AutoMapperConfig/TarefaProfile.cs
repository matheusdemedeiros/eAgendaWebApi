﻿using AutoMapper;
using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloTarefa;
using eAgenda.Webapi.ViewModels.ModuloTarefa;

namespace eAgenda.Webapi.Config.AutoMapperConfig
{
    public class TarefaProfile : Profile
    {

        public TarefaProfile()
        {
            ConverterEntidadeParaViewModel();

            ConverterViewModelParaEntidade();
        }

        private void ConverterViewModelParaEntidade()
        {
            CreateMap<InserirTarefaViewModel, Tarefa>()
                .ForMember(destino => destino.Itens, opt => opt.Ignore())
                .AfterMap((viewModel, tarefa) =>
                {
         
                    if(viewModel.Itens == null || viewModel.Itens.Count == 0)
                    {
                        return;
                    }
                    
                    foreach (var itemVM in viewModel.Itens)
                    {
                        var item = new ItemTarefa();
                        item.Titulo = itemVM.Titulo;
                        tarefa.AdicionarItem(item);
                    }
                });


            CreateMap<EditarTarefaViewModel, Tarefa>()
                .ForMember(destino => destino.Itens, opt => opt.Ignore())
                .AfterMap((viewModel, tarefa) =>
                {
                    foreach (var itemVM in viewModel.Itens)
                    {
                        if (itemVM.Concluido)
                        {
                            tarefa.ConcluirItem(itemVM.Id);
                        }
                        else
                        {
                            tarefa.MarcarPendente(itemVM.Id);
                        }

                    }
                    foreach (var itemVM in viewModel.Itens)
                    {
                        if (itemVM.Status == StatusItemTarefa.Adicionado)
                        {
                            var item = new ItemTarefa(itemVM.Titulo);
                            tarefa.AdicionarItem(item);
                        }
                        else if (itemVM.Status == StatusItemTarefa.Removido)
                        {
                            tarefa.RemoverItem(itemVM.Id);
                        }
                    }
                });
        }

        private void ConverterEntidadeParaViewModel()
        {
            CreateMap<Tarefa, ListarTarefaViewModel>()
               .ForMember(destino => destino.Prioridade, opt => opt.MapFrom(origem => origem.Prioridade.GetDescription()))
               .ForMember(destino => destino.Situação, opt =>
                   opt.MapFrom(origem => origem.PercentualConcluido == 100 ? "Concluido" : "Pendente"));

            CreateMap<Tarefa, VisualizarTarefaViewModel>()
            .ForMember(destino => destino.Prioridade, opt => opt.MapFrom(origem => origem.Prioridade.GetDescription()))
            .ForMember(destino => destino.Situação, opt =>
                opt.MapFrom(origem => origem.PercentualConcluido == 100 ? "Concluido" : "Pendente"))
            .ForMember(destino => destino.QuantidadeItens, opt => opt.MapFrom(origem => origem.Itens.Count));

            CreateMap<ItemTarefa, VisualizarItemTarefaViewModel>()
            .ForMember(destino => destino.Situacao, opt =>
                opt.MapFrom(origem => origem.Concluido ? "Concluído" : "Pendente"));
        }
    }
}