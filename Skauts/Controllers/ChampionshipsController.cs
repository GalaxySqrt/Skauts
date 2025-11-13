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
    public class ChampionshipsController : ControllerBase
    {
        private readonly IChampionshipService _championshipService;

        public ChampionshipsController(IChampionshipService championshipService)
        {
            _championshipService = championshipService;
        }

        /// <summary>
        /// Obtém todos os campeonatos.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ChampionshipDto>))]
        public async Task<ActionResult<List<ChampionshipDto>>> ObterCampeonatos()
        {
            var campeonatos = await _championshipService.ObterCampeonatosAsync();
            return Ok(campeonatos);
        }

        /// <summary>
        /// Obtém um campeonato específico pelo seu ID.
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ChampionshipDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ChampionshipDto>> ObterCampeonatoPorId(int id)
        {
            var campeonato = await _championshipService.ObterCampeonatoPorIdAsync(id);
            if (campeonato == null)
            {
                return NotFound("Campeonato não encontrado.");
            }
            return Ok(campeonato);
        }

        /// <summary>
        /// Obtém campeonatos de uma organização específica.
        /// </summary>
        [HttpGet("por-organizacao/{orgId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ChampionshipDto>))]
        public async Task<ActionResult<List<ChampionshipDto>>> ObterCampeonatosPorOrg(int orgId)
        {
            var campeonatos = await _championshipService.ObterCampeonatosPorOrgAsync(orgId);
            return Ok(campeonatos);
        }

        /// <summary>
        /// Obtém um campeonato pelo seu nome.
        /// </summary>
        [HttpGet("por-nome/{nome}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ChampionshipDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ChampionshipDto>> ObterCampeonatoPorNome(string nome)
        {
            var campeonato = await _championshipService.ObterCampeonatoPorNomeAsync(nome);
            if (campeonato == null)
            {
                return NotFound("Campeonato não encontrado.");
            }
            return Ok(campeonato);
        }

        /// <summary>
        /// Adiciona um novo campeonato.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ChampionshipDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AdicionarCampeonato([FromBody] SalvarChampionshipDto championshipDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var novoCampeonato = await _championshipService.AdicionarCampeonatoAsync(championshipDto);

            return CreatedAtAction(nameof(ObterCampeonatoPorId), new { id = novoCampeonato.Id }, novoCampeonato);
        }

        /// <summary>
        /// Atualiza um campeonato existente.
        /// </summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AtualizarCampeonato(int id, [FromBody] SalvarChampionshipDto championshipDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sucesso = await _championshipService.AtualizarCampeonatoAsync(id, championshipDto);
            if (!sucesso)
            {
                return NotFound("Campeonato não encontrado para atualização.");
            }

            return NoContent();
        }

        /// <summary>
        /// Exclui um campeonato.
        /// </summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ExcluirCampeonato(int id)
        {
            var sucesso = await _championshipService.ExcluirCampeonatoAsync(id);
            if (!sucesso)
            {
                return NotFound("Campeonato não encontrado para exclusão.");
            }

            return NoContent();
        }
    }
}