using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace eAgenda.Webapi.Controllers
{

    [ApiController]
    public abstract class eAgendaControllerBase : ControllerBase
    {
        protected ActionResult InternalError<T>(Result<T> registroResult)
        {
            return StatusCode(500, new
            {
                sucesso = false,
                erros = registroResult.Errors.Select(x => x.Message)
            });
        }
        protected ActionResult BadRequest<T>(Result<T> registroResult)
        {
            return StatusCode(300, new
            {
                sucesso = false,
                erros = registroResult.Errors.Select(x => x.Message)
            });
        }

        protected ActionResult NotFound<T>(Result<T> registroResult)
        {
            return StatusCode(404, new
            {
                sucesso = false,
                erros = registroResult.Errors.Select(x => x.Message)
            });
        }

        protected static bool RegistroNaoEncontrado<T>(Result<T> registroResult, string textoProcurado)
        {
            return registroResult.Errors.Any(x => x.Message.Contains(textoProcurado));
        }
    }
}
