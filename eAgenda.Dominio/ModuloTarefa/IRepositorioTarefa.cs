using eAgenda.Dominio.Compartilhado;
using System.Collections.Generic;
using System;

namespace eAgenda.Dominio.ModuloTarefa
{
    public interface IRepositorioTarefa : IRepositorio<Tarefa>
    {
        List<Tarefa> SelecionarTodos(StatusTarefaEnum status, Guid guid);
    }
}