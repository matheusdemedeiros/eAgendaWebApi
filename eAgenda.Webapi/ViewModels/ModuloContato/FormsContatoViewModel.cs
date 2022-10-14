using System.ComponentModel.DataAnnotations;
using System;

namespace eAgenda.Webapi.ViewModels.ModuloContato
{
    public class FormsContatoViewModel
    {
        
        public Guid Id { get; set; }
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Telefone { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Empresa { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Cargo { get; set; }
    }

    public class InserirContatoViewModel : FormsContatoViewModel
    {
    }

    public class EditarContatoViewModel : FormsContatoViewModel
    {
    }

}