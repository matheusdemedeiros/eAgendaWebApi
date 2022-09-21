using System;

namespace eAgenda.Webapi.ViewModels
{
    public class ListarTarefaViewModel
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Prioridade { get; set; }
        public string Situação { get; set; }
    }
}
