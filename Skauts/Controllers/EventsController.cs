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
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        /// <summary>
        /// Obtém todos os eventos.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EventDto>))]
        public async Task<ActionResult<List<EventDto>>> ObterEventos()
        {
            var eventos = await _eventService.ObterEventosAsync();
            return Ok(eventos);
        }

        /// <summary>
        /// Obtém um evento específico pelo seu ID.
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EventDto>> ObterEventoPorId(int id)
        {
            var evento = await _eventService.ObterEventoPorIdAsync(id);
            if (evento == null)
            {
                return NotFound("Evento não encontrado.");
            }
            return Ok(evento);
        }

        /// <summary>
        /// Obtém todos os eventos de uma partida específica.
        /// </summary>
        [HttpGet("por-partida/{matchId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EventDto>))]
        public async Task<ActionResult<List<EventDto>>> ObterEventosPorPartida(int matchId)
        {
            var eventos = await _eventService.ObterEventosPorPartidaAsync(matchId);
            return Ok(eventos);
        }

        /// <summary>
        /// Obtém todos os eventos de um jogador específico.
        /// </summary>
        [HttpGet("por-jogador/{playerId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EventDto>))]
        public async Task<ActionResult<List<EventDto>>> ObterEventosPorJogador(int playerId)
        {
            var eventos = await _eventService.ObterEventosPorJogadorAsync(playerId);
            return Ok(eventos);
        }

        /// <summary>
        /// Obtém todos os eventos de um tipo específico (ex: "Gol").
        /// </summary>
        [HttpGet("por-tipo/{eventTypeId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EventDto>))]
        public async Task<ActionResult<List<EventDto>>> ObterEventosPorTipo(int eventTypeId)
        {
            var eventos = await _eventService.ObterEventosPorTipoAsync(eventTypeId);
            return Ok(eventos);
        }

        /// <summary>
        /// Adiciona um novo evento.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EventDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AdicionarEvento([FromBody] SalvarEventDto eventDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var novoEvento = await _eventService.AdicionarEventoAsync(eventDto);

            // Retorna 201 Created com o novo objeto e um link para ele
            return CreatedAtAction(nameof(ObterEventoPorId), new { id = novoEvento.Id }, novoEvento);
        }

        /// <summary>
        /// Atualiza um evento existente.
        /// </summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AtualizarEvento(int id, [FromBody] SalvarEventDto eventDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sucesso = await _eventService.AtualizarEventoAsync(id, eventDto);
            if (!sucesso)
            {
                return NotFound("Evento não encontrado para atualização.");
            }

            return NoContent(); // Retorno 204 para sucesso em PUT
        }

        /// <summary>
        /// Exclui um evento.
        /// </summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ExcluirEvento(int id)
        {
            var sucesso = await _eventService.ExcluirEventoAsync(id);
            if (!sucesso)
            {
                return NotFound("Evento não encontrado para exclusão.");
            }

            return NoContent(); // Retorno 204 para sucesso em DELETE
        }
    }
}