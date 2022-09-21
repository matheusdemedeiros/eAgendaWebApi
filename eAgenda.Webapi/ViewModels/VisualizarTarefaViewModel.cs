using System;
using System.Collections.Generic;

namespace eAgenda.Webapi.ViewModels
{
    public class VisualizarTarefaViewModel
    {
        public VisualizarTarefaViewModel()
        {
            itens = new List<VisualizarItemTarefaViewModel>();
        }

        public string Titulo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataConclusao { get; set; }
        public int QuantidadeItens { get; set; }
        public decimal PercentualConcluido { get; set; }
        public string Prioridade { get; set; }
        public string Situação { get; set; }
        public List<VisualizarItemTarefaViewModel> itens { get; set; }
    }
}
