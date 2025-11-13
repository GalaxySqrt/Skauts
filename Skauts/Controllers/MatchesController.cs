using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skauts.DTOs;
using Skauts.Services.Interfaces; // Injetando a interface
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skauts.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MatchesController : ControllerBase
    {
        private readonly IMatchService _matchService;

        public MatchesController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        /// <summary>
        /// Obtém todas as partidas.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MatchDto>))]
        public async Task<ActionResult<List<MatchDto>>> ObterMatches()
        {
            var matches = await _matchService.ObterMatchesAsync();
            return Ok(matches);
        }

        /// <summary>
        /// Obtém uma partida específica pelo seu ID.
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MatchDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MatchDto>> ObterMatchPorId(int id)
        {
            var match = await _matchService.ObterMatchPorIdAsync(id);
            if (match == null)
            {
                return NotFound("Partida não encontrada.");
            }
            return Ok(match);
        }

        /// <summary>
        /// Obtém partidas de uma organização específica.
        /// </summary>
        [HttpGet("por-organizacao/{orgId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MatchDto>))]
        public async Task<ActionResult<List<MatchDto>>> ObterMatchesPorOrg(int orgId)
        {
            var matches = await _matchService.ObterMatchesPorOrgAsync(orgId);
            return Ok(matches);
        }

        /// <summary>
        /// Obtém partidas de um campeonato específico.
        /// </summary>
        [HttpGet("por-campeonato/{championshipId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MatchDto>))]
        public async Task<ActionResult<List<MatchDto>>> ObterMatchesPorCampeonato(int championshipId)
        {
            var matches = await _matchService.ObterMatchesPorCampeonatoAsync(championshipId);
            return Ok(matches);
        }

        /// <summary>
        /// Obtém partidas de um time específico (Time A ou Time B).
        /// </summary>
        [HttpGet("por-time/{teamId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MatchDto>))]
        public async Task<ActionResult<List<MatchDto>>> ObterMatchesPorTime(Guid teamId)
        {
            var matches = await _matchService.ObterMatchesPorTimeAsync(teamId);
            return Ok(matches);
        }

        /// <summary>
        /// Obtém partidas em uma data específica (formato YYYY-MM-DD).
        /// </summary>
        [HttpGet("por-data")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MatchDto>))]
        public async Task<ActionResult<List<MatchDto>>> ObterMatchesPorData([FromQuery] DateTime data)
        {
            // O serviço já busca pelo .Date.Date
            var matches = await _matchService.ObterMatchesPorDataAsync(data);
            return Ok(matches);
        }

        /// <summary>
        /// Adiciona uma nova partida.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MatchDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AdicionarMatch([FromBody] SalvarMatchDto matchDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var novaMatch = await _matchService.AdicionarMatchAsync(matchDto);

            return CreatedAtAction(nameof(ObterMatchPorId), new { id = novaMatch.Id }, novaMatch);
        }

        /// <summary>
        /// Atualiza uma partida existente.
        /// </summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AtualizarMatch(int id, [FromBody] SalvarMatchDto matchDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sucesso = await _matchService.AtualizarMatchAsync(id, matchDto);
            if (!sucesso)
            {
                return NotFound("Partida não encontrada para atualização.");
            }

            return NoContent();
        }

        /// <summary>
        /// Exclui uma partida.
        /// </summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ExcluirMatch(int id)
        {
            var sucesso = await _matchService.ExcluirMatchAsync(id);
            if (!sucesso)
            {
                return NotFound("Partida não encontrada para exclusão.");
            }

            return NoContent();
        }
    }
}