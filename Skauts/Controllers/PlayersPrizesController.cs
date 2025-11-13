using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skauts.DTOs;
using Skauts.Services.Interfaces; // Injetando a interface
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skauts.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersPrizesController : ControllerBase
    {
        private readonly IPlayersPrizeService _playersPrizeService;

        public PlayersPrizesController(IPlayersPrizeService playersPrizeService)
        {
            _playersPrizeService = playersPrizeService;
        }

        /// <summary>
        /// Obtém todos os prêmios de jogadores.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PlayersPrizeDto>))]
        public async Task<ActionResult<List<PlayersPrizeDto>>> ObterPremiosDeJogadores()
        {
            var premios = await _playersPrizeService.ObterPremiosDeJogadoresAsync();
            return Ok(premios);
        }

        /// <summary>
        /// Obtém um prêmio de jogador específico pelo seu ID.
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlayersPrizeDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlayersPrizeDto>> ObterPremioDeJogadorPorId(int id)
        {
            var premio = await _playersPrizeService.ObterPremioDeJogadorPorIdAsync(id);
            if (premio == null)
            {
                return NotFound("Prêmio não encontrado.");
            }
            return Ok(premio);
        }

        /// <summary>
        /// Obtém todos os prêmios de um jogador específico.
        /// </summary>
        [HttpGet("por-jogador/{playerId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PlayersPrizeDto>))]
        public async Task<ActionResult<List<PlayersPrizeDto>>> ObterPremiosPorJogador(int playerId)
        {
            var premios = await _playersPrizeService.ObterPremiosPorJogadorAsync(playerId);
            return Ok(premios);
        }

        /// <summary>
        /// Obtém todos os registros de prêmio por tipo (ex: todos "Artilheiros").
        /// </summary>
        [HttpGet("por-tipo-premio/{prizeTypeId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PlayersPrizeDto>))]
        public async Task<ActionResult<List<PlayersPrizeDto>>> ObterPremiosPorTipoDePremio(int prizeTypeId)
        {
            var premios = await _playersPrizeService.ObterPremiosPorTipoDePremioAsync(prizeTypeId);
            return Ok(premios);
        }

        /// <summary>
        /// Adiciona um novo prêmio para um jogador.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PlayersPrizeDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AdicionarPremioDeJogador([FromBody] SalvarPlayersPrizeDto prizeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var novoPremio = await _playersPrizeService.AdicionarPremioDeJogadorAsync(prizeDto);

            return CreatedAtAction(nameof(ObterPremioDeJogadorPorId), new { id = novoPremio.Id }, novoPremio);
        }

        /// <summary>
        /// Atualiza um prêmio de jogador existente.
        /// </summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AtualizarPremioDeJogador(int id, [FromBody] SalvarPlayersPrizeDto prizeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sucesso = await _playersPrizeService.AtualizarPremioDeJogadorAsync(id, prizeDto);
            if (!sucesso)
            {
                return NotFound("Prêmio não encontrado para atualização.");
            }

            return NoContent();
        }

        /// <summary>
        /// Exclui um prêmio de jogador.
        /// </summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ExcluirPremioDeJogador(int id)
        {
            var sucesso = await _playersPrizeService.ExcluirPremioDeJogadorAsync(id);
            if (!sucesso)
            {
                return NotFound("Prêmio não encontrado para exclusão.");
            }

            return NoContent();
        }
    }
}