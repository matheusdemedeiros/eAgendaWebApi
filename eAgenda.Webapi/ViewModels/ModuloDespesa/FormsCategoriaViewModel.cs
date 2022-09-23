using eAgenda.Dominio.ModuloDespesa;
using System;
using System.ComponentModel.DataAnnotations;

namespace eAgenda.Webapi.ViewModels.ModuloDespesa
{
    public class FormsCategoriaViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Titulo { get; set; }

        public StatusCategoriaEnum Status { get; set; }
    }
}
