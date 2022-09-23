using AutoMapper;
using eAgenda.Aplicacao.ModuloContato;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Webapi.ViewModels.ModuloContato;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eAgenda.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatosController : ControllerBase
    {
        private readonly ServicoContato servicoContato;
        private readonly IMapper mapeadorContatos;

        public ContatosController(ServicoContato servicoContato, IMapper mapeadorContatos)
        {
            this.servicoContato = servicoContato;
            this.mapeadorContatos = mapeadorContatos;
        }

        [HttpGet]
        public ActionResult<List<ListarContatoViewModel>> SelecionarTodos()
        {
            var contatoResult = servicoContato.SelecionarTodos();

            if (contatoResult.IsFailed)
            {
                return StatusCode(500, new
                {
                    sucesso = false,
                    erros = contatoResult.Errors.Select(x => x.Message)
                });
            }

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorContatos.Map<List<ListarContatoViewModel>>(contatoResult.Value)
            });
            
        }


        [HttpGet("visualizar-completo/{id:guid}")]
        public ActionResult<VisualizarContatoViewModel> SelecionarPorId(Guid id)
        {

            var contatoResult = servicoContato.SelecionarPorId(id);

            if (contatoResult.Errors.Any(x => x.Message.Contains("não encontrado")))
            {
                return NotFound(new
                {
                    sucesso = false,
                    erros = contatoResult.Errors.Select(x => x.Message)
                });
            }

            if (contatoResult.IsFailed)
            {
                return StatusCode(500, new
                {
                    sucesso = false,
                    erros = contatoResult.Errors.Select(x => x.Message)
                });
            }

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorContatos.Map<VisualizarContatoViewModel>(contatoResult.Value)
            });
        }

        [HttpPost]
        public ActionResult<FormsContatoViewModel> Inserir(FormsContatoViewModel contatoVM)
        {
            var listaErros = ModelState.Values
        .SelectMany(x => x.Errors)
        .Select(x => x.ErrorMessage);

            if (listaErros.Any())
            {
                return BadRequest(new
                {
                    sucesso = false,
                    erros = listaErros.ToList()
                });
            }

            var contato = mapeadorContatos.Map<Contato>(contatoVM);

            var contatoResult = servicoContato.Inserir(contato);

            if (contatoResult.IsFailed)
            {
                return StatusCode(500, new
                {
                    sucesso = false,
                    erros = contatoResult.Errors.Select(x => x.Message)
                });
            }

            return Ok(new
            {
                sucesso = true,
                dados = contatoVM
            });
        }

        [HttpPut("{id:guid}")]
        public ActionResult<FormsContatoViewModel> Editar(Guid id, FormsContatoViewModel contatoVM)
        {
            var listaErros = ModelState.Values
        .SelectMany(x => x.Errors)
        .Select(x => x.ErrorMessage);

            if (listaErros.Any())
            {
                return BadRequest(new
                {
                    sucesso = false,
                    erros = listaErros.ToList()
                });
            }

            var contatoResult = servicoContato.SelecionarPorId(id);

            if (contatoResult.Errors.Any(x => x.Message.Contains("não encontrado")))
            {
                return NotFound(
                    new
                    {
                        sucesso = false,
                        erros = contatoResult.Errors.Select(x => x.Message)
                    });
            }

            var contato = mapeadorContatos.Map(contatoVM, contatoResult.Value);

            contatoResult = servicoContato.Editar(contato);

            if (contatoResult.IsFailed)
            {
                return StatusCode(500, new
                {
                    sucesso = false,
                    erros = contatoResult.Errors.Select(x => x.Message)
                });
            }

            return Ok(new
            {
                sucesso = true,
                dados = contatoVM
            });
        }

        [HttpDelete("{id:guid}")]
        public ActionResult Excluir(Guid id)
        {

            var contatoResult = servicoContato.Excluir(id);

            if (contatoResult.Errors.Any(x => x.Message.Contains("não encontrado")))
            {
                return NotFound(
                    new
                    {
                        sucesso = false,
                        erros = contatoResult.Errors.Select(x => x.Message)
                    });
            }

            if (contatoResult.IsFailed)
            {
                return StatusCode(500, new
                {
                    sucesso = false,
                    erros = contatoResult.Errors.Select(x => x.Message)
                });
            }

            return NoContent();
        }
    }
}
