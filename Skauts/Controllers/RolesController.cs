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
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Obtém todas as funções/posições (Roles).
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RoleDto>))]
        public async Task<ActionResult<List<RoleDto>>> ObterRoles()
        {
            var roles = await _roleService.ObterRolesAsync();
            return Ok(roles);
        }

        /// <summary>
        /// Obtém uma função específica pelo seu ID.
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoleDto>> ObterRolePorId(int id)
        {
            var role = await _roleService.ObterRolePorIdAsync(id);
            if (role == null)
            {
                return NotFound("Função não encontrada.");
            }
            return Ok(role);
        }

        /// <summary>
        /// Obtém uma função pelo seu nome (ex: "Atacante").
        /// </summary>
        [HttpGet("por-nome/{nome}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoleDto>> ObterRolePorNome(string nome)
        {
            var role = await _roleService.ObterRolePorNomeAsync(nome);
            if (role == null)
            {
                return NotFound("Função não encontrada.");
            }
            return Ok(role);
        }

        /// <summary>
        /// Obtém uma função pela sua sigla (ex: "ATA").
        /// </summary>
        [HttpGet("por-acronimo/{acronimo}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoleDto>> ObterRolePorAcronimo(string acronimo)
        {
            var role = await _roleService.ObterRolePorAcronimoAsync(acronimo);
            if (role == null)
            {
                return NotFound("Função não encontrada.");
            }
            return Ok(role);
        }

        /// <summary>
        /// Adiciona uma nova função.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RoleDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AdicionarRole([FromBody] SalvarRoleDto roleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var novaRole = await _roleService.AdicionarRoleAsync(roleDto);

            return CreatedAtAction(nameof(ObterRolePorId), new { id = novaRole.Id }, novaRole);
        }

        /// <summary>
        /// Atualiza uma função existente.
        /// </summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AtualizarRole(int id, [FromBody] SalvarRoleDto roleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sucesso = await _roleService.AtualizarRoleAsync(id, roleDto);
            if (!sucesso)
            {
                return NotFound("Função não encontrada para atualização.");
            }

            return NoContent();
        }

        /// <summary>
        /// Exclui uma função.
        /// </summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ExcluirRole(int id)
        {
            var sucesso = await _roleService.ExcluirRoleAsync(id);
            if (!sucesso)
            {
                return NotFound("Função não encontrada para exclusão.");
            }

            return NoContent();
        }
    }
}