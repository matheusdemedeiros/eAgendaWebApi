using eAgenda.Webapi.ViewModels.ModuloContato;
using System;

namespace eAgenda.Webapi.ViewModels.ModuloCompromisso
{
    public class ListarCompromissoViewModel
    {
        public Guid Id { get; set; }
        public string Assunto { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public VisualizarContatoViewModel? Contato { get; set; }
    }
}
