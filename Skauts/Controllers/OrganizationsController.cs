using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skauts.DTOs;
using Skauts.Services.Interfaces; // Injetando a interface, não a classe
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skauts.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationsController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        /// <summary>
        /// Obtém todas as organizações.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OrganizationDto>))]
        public async Task<ActionResult<List<OrganizationDto>>> ObterOrganizacoes()
        {
            var organizacoes = await _organizationService.ObterOrganizacoesAsync();
            return Ok(organizacoes);
        }

        /// <summary>
        /// Obtém uma organização específica pelo seu ID.
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrganizationDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrganizationDto>> ObterOrganizacaoPorId(int id)
        {
            var organizacao = await _organizationService.ObterOrganizacaoPorIdAsync(id);
            if (organizacao == null)
            {
                return NotFound("Organização não encontrada.");
            }
            return Ok(organizacao);
        }

        /// <summary>
        /// Obtém uma organização pelo seu nome.
        /// </summary>
        [HttpGet("por-nome/{nome}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrganizationDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrganizationDto>> ObterOrganizacaoPorNome(string nome)
        {
            var organizacao = await _organizationService.ObterOrganizacaoPorNomeAsync(nome);
            if (organizacao == null)
            {
                return NotFound("Organização não encontrada.");
            }
            return Ok(organizacao);
        }

        /// <summary>
        /// Adiciona uma nova organização.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OrganizationDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AdicionarOrganizacao([FromBody] SalvarOrganizationDto orgDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var novaOrganizacao = await _organizationService.AdicionarOrganizacaoAsync(orgDto);

            // Retorna 201 Created com o novo objeto e um link para ele
            return CreatedAtAction(nameof(ObterOrganizacaoPorId), new { id = novaOrganizacao.Id }, novaOrganizacao);
        }

        /// <summary>
        /// Atualiza uma organização existente.
        /// </summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AtualizarOrganizacao(int id, [FromBody] SalvarOrganizationDto orgDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sucesso = await _organizationService.AtualizarOrganizacaoAsync(id, orgDto);
            if (!sucesso)
            {
                return NotFound("Organização não encontrada para atualização.");
            }

            return NoContent(); // Retorno 204 para sucesso em PUT
        }

        /// <summary>
        /// Exclui uma organização.
        /// </summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ExcluirOrganizacao(int id)
        {
            var sucesso = await _organizationService.ExcluirOrganizacaoAsync(id);
            if (!sucesso)
            {
                return NotFound("Organização não encontrada para exclusão.");
            }

            return NoContent(); // Retorno 204 para sucesso em DELETE
        }
    }
}