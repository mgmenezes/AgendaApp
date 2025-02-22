// AgendaApp.API/Controllers/ContatosController.cs
using Microsoft.AspNetCore.Mvc;
using AgendaApp.Application.DTOs;
using AgendaApp.Application.Interfaces;

namespace AgendaApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContatosController : ControllerBase  // Note que o nome da classe é ContatosController
    {
        private readonly IContatoService _contatoService;

        public ContatosController(IContatoService contatoService)  // O construtor deve ter o mesmo nome da classe
        {
            _contatoService = contatoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContatoDto>>> ObterTodos()
        {
            var contatos = await _contatoService.ObterTodosAsync();
            return Ok(contatos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContatoDto>> ObterPorId(Guid id)
        {
            var contato = await _contatoService.ObterPorIdAsync(id);
            return Ok(contato);
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<ContatoDto>> Criar([FromBody] CriarContatoDto dto)
        {
            var contato = await _contatoService.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = contato.Id }, contato);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ContatoDto>> Atualizar(Guid id, [FromBody] AtualizarContatoDto dto)
        {
            if (id != dto.Id)
                return BadRequest("O Id da rota não corresponde ao Id do contato");

            var contato = await _contatoService.AtualizarAsync(dto);
            return Ok(contato);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Inativar(Guid id)
        {
            await _contatoService.InativarAsync(id);
            return NoContent();
        }
    }
}