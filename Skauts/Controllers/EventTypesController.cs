using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skauts.DTOs;
using Skauts.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skauts.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EventTypesController : ControllerBase
    {
        private readonly IEventTypeService _eventTypeService;

        public EventTypesController(IEventTypeService eventTypeService)
        {
            _eventTypeService = eventTypeService;
        }

        /// <summary>
        /// Obtém todos os tipos de evento.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EventTypeDto>))]
        public async Task<ActionResult<List<EventTypeDto>>> ObterTiposDeEvento()
        {
            var tipos = await _eventTypeService.ObterTiposDeEventoAsync();
            return Ok(tipos);
        }

        /// <summary>
        /// Obtém um tipo de evento específico pelo seu ID.
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventTypeDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EventTypeDto>> ObterTipoDeEventoPorId(int id)
        {
            var tipo = await _eventTypeService.ObterTipoDeEventoPorIdAsync(id);
            if (tipo == null)
            {
                return NotFound("Tipo de evento não encontrado.");
            }
            return Ok(tipo);
        }

        /// <summary>
        /// Obtém um tipo de evento pelo seu nome.
        /// </summary>
        [HttpGet("por-nome/{nome}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventTypeDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EventTypeDto>> ObterTipoDeEventoPorNome(string nome)
        {
            var tipo = await _eventTypeService.ObterTipoDeEventoPorNomeAsync(nome);
            if (tipo == null)
            {
                return NotFound("Tipo de evento não encontrado.");
            }
            return Ok(tipo);
        }

        /// <summary>
        /// Adiciona um novo tipo de evento.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EventTypeDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AdicionarTipoDeEvento([FromBody] SalvarEventTypeDto eventTypeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var novoTipo = await _eventTypeService.AdicionarTipoDeEventoAsync(eventTypeDto);

            return CreatedAtAction(nameof(ObterTipoDeEventoPorId), new { id = novoTipo.Id }, novoTipo);
        }

        /// <summary>
        /// Atualiza um tipo de evento existente.
        /// </summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AtualizarTipoDeEvento(int id, [FromBody] SalvarEventTypeDto eventTypeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sucesso = await _eventTypeService.AtualizarTipoDeEventoAsync(id, eventTypeDto);
            if (!sucesso)
            {
                return NotFound("Tipo de evento não encontrado para atualização.");
            }

            return NoContent();
        }

        /// <summary>
        /// Exclui um tipo de evento.
        /// </summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ExcluirTipoDeEvento(int id)
        {
            var sucesso = await _eventTypeService.ExcluirTipoDeEventoAsync(id);
            if (!sucesso)
            {
                return NotFound("Tipo de evento não encontrado para exclusão.");
            }

            return NoContent();
        }
    }
}