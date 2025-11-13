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
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        /// <summary>
        /// Obtém todos os jogadores.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PlayerDto>))]
        public async Task<ActionResult<List<PlayerDto>>> ObterPlayers()
        {
            var players = await _playerService.ObterPlayersAsync();
            return Ok(players);
        }

        /// <summary>
        /// Obtém um jogador específico pelo seu ID.
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlayerDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlayerDto>> ObterPlayerPorId(int id)
        {
            var player = await _playerService.ObterPlayerPorIdAsync(id);
            if (player == null)
            {
                return NotFound("Jogador não encontrado.");
            }
            return Ok(player);
        }

        /// <summary>
        /// Obtém jogadores de uma organização específica.
        /// </summary>
        [HttpGet("por-organizacao/{orgId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PlayerDto>))]
        public async Task<ActionResult<List<PlayerDto>>> ObterPlayersPorOrg(int orgId)
        {
            var players = await _playerService.ObterPlayersPorOrgAsync(orgId);
            return Ok(players);
        }

        /// <summary>
        /// Obtém um jogador pelo seu e-mail.
        /// </summary>
        [HttpGet("por-email")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlayerDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlayerDto>> ObterPlayerPorEmail([FromQuery] string email)
        {
            var player = await _playerService.ObterPlayerPorEmailAsync(email);
            if (player == null)
            {
                return NotFound("Jogador não encontrado com este e-mail.");
            }
            return Ok(player);
        }

        /// <summary>
        /// Verifica se um jogador existe com um e-mail específico.
        /// </summary>
        [HttpGet("existe-por-email")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<ActionResult<bool>> ExistePlayerPorEmail([FromQuery] string email)
        {
            var existe = await _playerService.ExistePlayerPorEmailAsync(email);
            return Ok(existe);
        }

        /// <summary>
        /// Adiciona um novo jogador.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PlayerDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AdicionarPlayer([FromBody] SalvarPlayerDto playerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verificação de e-mail duplicado
            if (!string.IsNullOrWhiteSpace(playerDto.Email) && await _playerService.ExistePlayerPorEmailAsync(playerDto.Email))
            {
                ModelState.AddModelError(nameof(SalvarPlayerDto.Email), "Este e-mail já está em uso.");
                return BadRequest(ModelState);
            }

            var novoPlayer = await _playerService.AdicionarPlayerAsync(playerDto);

            return CreatedAtAction(nameof(ObterPlayerPorId), new { id = novoPlayer.Id }, novoPlayer);
        }

        /// <summary>
        /// Atualiza um jogador existente.
        /// </summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AtualizarPlayer(int id, [FromBody] SalvarPlayerDto playerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sucesso = await _playerService.AtualizarPlayerAsync(id, playerDto);
            if (!sucesso)
            {
                return NotFound("Jogador não encontrado para atualização.");
            }

            return NoContent();
        }

        /// <summary>
        /// Exclui um jogador.
        /// </summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ExcluirPlayer(int id)
        {
            var sucesso = await _playerService.ExcluirPlayerAsync(id);
            if (!sucesso)
            {
                return NotFound("Jogador não encontrado para exclusão.");
            }

            return NoContent();
        }
    }
}