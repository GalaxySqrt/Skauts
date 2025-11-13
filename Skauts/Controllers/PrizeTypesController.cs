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
    public class PrizeTypesController : ControllerBase
    {
        private readonly IPrizeTypeService _prizeTypeService;

        public PrizeTypesController(IPrizeTypeService prizeTypeService)
        {
            _prizeTypeService = prizeTypeService;
        }

        /// <summary>
        /// Obtém todos os tipos de prêmio.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PrizeTypeDto>))]
        public async Task<ActionResult<List<PrizeTypeDto>>> ObterTiposDePremio()
        {
            var tipos = await _prizeTypeService.ObterTiposDePremioAsync();
            return Ok(tipos);
        }

        /// <summary>
        /// Obtém um tipo de prêmio específico pelo seu ID.
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PrizeTypeDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PrizeTypeDto>> ObterTipoDePremioPorId(int id)
        {
            var tipo = await _prizeTypeService.ObterTipoDePremioPorIdAsync(id);
            if (tipo == null)
            {
                return NotFound("Tipo de prêmio não encontrado.");
            }
            return Ok(tipo);
        }

        /// <summary>
        /// Obtém um tipo de prêmio pelo seu nome.
        /// </summary>
        [HttpGet("por-nome/{nome}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PrizeTypeDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PrizeTypeDto>> ObterTipoDePremioPorNome(string nome)
        {
            var tipo = await _prizeTypeService.ObterTipoDePremioPorNomeAsync(nome);
            if (tipo == null)
            {
                return NotFound("Tipo de prêmio não encontrado.");
            }
            return Ok(tipo);
        }

        /// <summary>
        /// Adiciona um novo tipo de prêmio.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PrizeTypeDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AdicionarTipoDePremio([FromBody] SalvarPrizeTypeDto prizeTypeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var novoTipo = await _prizeTypeService.AdicionarTipoDePremioAsync(prizeTypeDto);

            return CreatedAtAction(nameof(ObterTipoDePremioPorId), new { id = novoTipo.Id }, novoTipo);
        }

        /// <summary>
        /// Atualiza um tipo de prêmio existente.
        /// </summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AtualizarTipoDePremio(int id, [FromBody] SalvarPrizeTypeDto prizeTypeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sucesso = await _prizeTypeService.AtualizarTipoDePremioAsync(id, prizeTypeDto);
            if (!sucesso)
            {
                return NotFound("Tipo de prêmio não encontrado para atualização.");
            }

            return NoContent();
        }

        /// <summary>
        /// Exclui um tipo de prêmio.
        /// </summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ExcluirTipoDePremio(int id)
        {
            var sucesso = await _prizeTypeService.ExcluirTipoDePremioAsync(id);
            if (!sucesso)
            {
                return NotFound("Tipo de prêmio não encontrado para exclusão.");
            }

            return NoContent();
        }
    }
}