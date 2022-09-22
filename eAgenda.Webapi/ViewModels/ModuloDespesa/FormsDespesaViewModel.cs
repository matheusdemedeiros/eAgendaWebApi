using eAgenda.Dominio.ModuloDespesa;
using System;
using System.Collections.Generic;

namespace eAgenda.Webapi.ViewModels.ModuloDespesa
{
    public class FormsDespesaViewModel
    {
        public string Descricao { get; set; }

        public decimal Valor { get; set; }

        public DateTime Data { get; set; }

        public FormaPgtoDespesaEnum FormaPagamento { get; set; }

        public List<FormsCategoriaViewModel> Categorias { get; set; }
        //public List<Guid> CategoriasId { get; set; }

    }

    public class InserirDespesaViewModel : FormsDespesaViewModel
    {

    }
    public class EditarDespesaViewModel : FormsDespesaViewModel
    {

    }
}
