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
    public class UsersOrganizationsController : ControllerBase
    {
        private readonly IUsersOrganizationService _usersOrgService;

        public UsersOrganizationsController(IUsersOrganizationService usersOrgService)
        {
            _usersOrgService = usersOrgService;
        }

        /// <summary>
        /// Obtém todas as relações de usuários para uma organização (ex: listar admins).
        /// </summary>
        [HttpGet("por-organizacao/{orgId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UsersOrganizationDto>))]
        public async Task<ActionResult<List<UsersOrganizationDto>>> ObterUsuariosDaOrganizacao(int orgId)
        {
            var relacoes = await _usersOrgService.ObterUsuariosDaOrganizacaoAsync(orgId);
            return Ok(relacoes);
        }

        /// <summary>
        /// Obtém todas as organizações que um usuário gerencia.
        /// </summary>
        [HttpGet("por-usuario/{userId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UsersOrganizationDto>))]
        public async Task<ActionResult<List<UsersOrganizationDto>>> ObterOrganizacoesDoUsuario(int userId)
        {
            var relacoes = await _usersOrgService.ObterOrganizacoesDoUsuarioAsync(userId);
            return Ok(relacoes);
        }

        /// <summary>
        /// Obtém uma relação específica (usuário-organização).
        /// </summary>
        [HttpGet("relacao")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsersOrganizationDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UsersOrganizationDto>> ObterRelacao(
            [FromQuery] int userId, [FromQuery] int orgId)
        {
            var relacao = await _usersOrgService.ObterRelacaoAsync(userId, orgId);
            if (relacao == null)
            {
                return NotFound("Relação não encontrada.");
            }
            return Ok(relacao);
        }

        /// <summary>
        /// Adiciona um usuário a uma organização (definindo se é admin).
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UsersOrganizationDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AdicionarUsuarioNaOrganizacao(
            [FromBody] UsersOrganizationDto usersOrgDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var novaRelacao = await _usersOrgService.AdicionarUsuarioNaOrganizacaoAsync(usersOrgDto);

            // Retorna 201 Created com o novo objeto e um link para ele
            return CreatedAtAction(nameof(ObterRelacao),
                new { userId = novaRelacao.UserId, orgId = novaRelacao.OrgId },
                novaRelacao);
        }

        /// <summary>
        /// Atualiza uma relação (ex: transformar um usuário em admin).
        /// </summary>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AtualizarRelacaoUsuarioOrganizacao(
            [FromBody] UsersOrganizationDto usersOrgDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sucesso = await _usersOrgService.AtualizarRelacaoUsuarioOrganizacaoAsync(usersOrgDto);
            if (!sucesso)
            {
                return NotFound("Relação não encontrada para atualização.");
            }

            return NoContent();
        }

        /// <summary>
        /// Remove um usuário de uma organização.
        /// </summary>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoverUsuarioDaOrganizacao(
            [FromQuery] int userId, [FromQuery] int orgId)
        {
            var sucesso = await _usersOrgService.RemoverUsuarioDaOrganizacaoAsync(userId, orgId);
            if (!sucesso)
            {
                return NotFound("Relação não encontrada para exclusão.");
            }

            return NoContent();
        }
    }
}