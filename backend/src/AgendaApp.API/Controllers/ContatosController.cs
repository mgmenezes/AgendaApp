using Microsoft.AspNetCore.Mvc;
using AgendaApp.Application.Commands.Contatos.CriarContato;
using AgendaApp.Application.Queries.Contatos.ObterContatos;
using AgendaApp.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using AgendaApp.Application.Commands.Contatos.InativarContato;
using AgendaApp.Application.Queries.Contatos.ObterContatoPorId;
using AgendaApp.Application.Commands.Contatos.AtualizarContato;
namespace AgendaApp.API.Controllers
{   
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ContatosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContatosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContatoDto>>> Get()
        {
            var query = new ObterContatosQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CriarContatoCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = result }, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Inativar(Guid id)
        {
            var command = new InativarContatoCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContatoDto>> GetById(Guid id)
        {
            var query = new ObterContatoPorIdQuery { Id = id };
            var result = await _mediator.Send(query);
            
            if (result == null)
                return NotFound();
        
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AtualizarContatoCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            var result = await _mediator.Send(command);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
} 