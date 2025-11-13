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
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamsController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        /// <summary>
        /// Obtém todos os times.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TeamDto>))]
        public async Task<ActionResult<List<TeamDto>>> ObterTimes()
        {
            var times = await _teamService.ObterTimesAsync();
            return Ok(times);
        }

        /// <summary>
        /// Obtém um time específico pelo seu ID (GUID).
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeamDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TeamDto>> ObterTimePorId(Guid id)
        {
            var time = await _teamService.ObterTimePorIdAsync(id);
            if (time == null)
            {
                return NotFound("Time não encontrado.");
            }
            return Ok(time);
        }

        /// <summary>
        /// Obtém times de uma organização específica.
        /// </summary>
        [HttpGet("por-organizacao/{orgId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TeamDto>))]
        public async Task<ActionResult<List<TeamDto>>> ObterTimesPorOrg(int orgId)
        {
            var times = await _teamService.ObterTimesPorOrgAsync(orgId);
            return Ok(times);
        }

        /// <summary>
        /// Obtém um time pelo seu nome.
        /// </summary>
        [HttpGet("por-nome/{nome}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeamDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TeamDto>> ObterTimePorNome(string nome)
        {
            var time = await _teamService.ObterTimePorNomeAsync(nome);
            if (time == null)
            {
                return NotFound("Time não encontrado.");
            }
            return Ok(time);
        }

        /// <summary>
        /// Adiciona um novo time.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TeamDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AdicionarTime([FromBody] SalvarTeamDto teamDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var novoTime = await _teamService.AdicionarTimeAsync(teamDto);

            return CreatedAtAction(nameof(ObterTimePorId), new { id = novoTime.Id }, novoTime);
        }

        /// <summary>
        /// Atualiza um time existente.
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AtualizarTime(Guid id, [FromBody] SalvarTeamDto teamDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sucesso = await _teamService.AtualizarTimeAsync(id, teamDto);
            if (!sucesso)
            {
                return NotFound("Time não encontrado para atualização.");
            }

            return NoContent();
        }

        /// <summary>
        /// Exclui um time.
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ExcluirTime(Guid id)
        {
            var sucesso = await _teamService.ExcluirTimeAsync(id);
            if (!sucesso)
            {
                return NotFound("Time não encontrado para exclusão.");
            }

            return NoContent();
        }
    }
}