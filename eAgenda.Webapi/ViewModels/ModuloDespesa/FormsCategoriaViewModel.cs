using eAgenda.Dominio.ModuloDespesa;
using System;

namespace eAgenda.Webapi.ViewModels.ModuloDespesa
{
    public class FormsCategoriaViewModel
    {
        public Guid Id { get; set; }

        public string Titulo { get; set; }

        public StatusCategoriaEnum Status { get; set; }
    }
}
