using eAgenda.Dominio.ModuloCompromisso;
using System;
using System.ComponentModel.DataAnnotations;

namespace eAgenda.Webapi.ViewModels.ModuloCompromisso
{
    public class FormsCompromissoViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Assunto { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime Data { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public TimeSpan HoraInicio { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public TimeSpan HoraTermino { get; set; }
        public Guid ContatoId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public  TipoLocalizacaoCompromissoEnum TipoLocal { get; set; }
        public string Local { get; set; }
        public string Link { get; set; }
    }
}