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
    public class TeamPlayersController : ControllerBase
    {
        private readonly ITeamPlayerService _teamPlayerService;

        public TeamPlayersController(ITeamPlayerService teamPlayerService)
        {
            _teamPlayerService = teamPlayerService;
        }

        /// <summary>
        /// Obtém todos os jogadores de um time (com suas datas de entrada).
        /// </summary>
        [HttpGet("por-time/{teamId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TeamPlayerDto>))]
        public async Task<ActionResult<List<TeamPlayerDto>>> ObterJogadoresDoTime(Guid teamId)
        {
            var relacoes = await _teamPlayerService.ObterJogadoresDoTimeAsync(teamId);
            return Ok(relacoes);
        }

        /// <summary>
        /// Obtém todos os times em que um jogador esteve.
        /// </summary>
        [HttpGet("por-jogador/{playerId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TeamPlayerDto>))]
        public async Task<ActionResult<List<TeamPlayerDto>>> ObterTimesDoJogador(int playerId)
        {
            var relacoes = await _teamPlayerService.ObterTimesDoJogadorAsync(playerId);
            return Ok(relacoes);
        }

        /// <summary>
        /// Obtém uma relação time-jogador específica.
        /// </summary>
        [HttpGet("relacao")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeamPlayerDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TeamPlayerDto>> ObterRelacao(
            [FromQuery] Guid teamId, [FromQuery] int playerId)
        {
            var relacao = await _teamPlayerService.ObterRelacaoAsync(teamId, playerId);
            if (relacao == null)
            {
                return NotFound("Relação não encontrada.");
            }
            return Ok(relacao);
        }

        /// <summary>
        /// Adiciona um jogador a um time (com data de entrada).
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TeamPlayerDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AdicionarJogadorAoTime(
            [FromBody] TeamPlayerDto teamPlayerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var novaRelacao = await _teamPlayerService.AdicionarJogadorAoTimeAsync(teamPlayerDto);

            return CreatedAtAction(nameof(ObterRelacao),
                new { teamId = novaRelacao.TeamId, playerId = novaRelacao.PlayerId },
                novaRelacao);
        }

        /// <summary>
        /// Atualiza uma relação (ex: corrigir a data de entrada).
        /// </summary>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AtualizarRelacao(
            [FromBody] TeamPlayerDto teamPlayerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sucesso = await _teamPlayerService.AtualizarRelacaoAsync(teamPlayerDto);
            if (!sucesso)
            {
                return NotFound("Relação não encontrada para atualização.");
            }

            return NoContent();
        }

        /// <summary>
        /// Remove um jogador de um time.
        /// </summary>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoverJogadorDoTime(
            [FromQuery] Guid teamId, [FromQuery] int playerId)
        {
            var sucesso = await _teamPlayerService.RemoverJogadorDoTimeAsync(teamId, playerId);
            if (!sucesso)
            {
                return NotFound("Relação não encontrada para exclusão.");
            }

            return NoContent();
        }
    }
}