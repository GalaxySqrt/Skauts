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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Obtém todos os usuários.
        /// </summary>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserDto>))]
        public async Task<ActionResult<List<UserDto>>> ObterUsuarios()
        {
            var usuarios = await _userService.ObterUsuariosAsync();
            return Ok(usuarios);
        }

        /// <summary>
        /// Obtém um usuário específico pelo seu ID.
        /// </summary>

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> ObterUsuarioPorId(int id)
        {
            var usuario = await _userService.ObterUsuarioPorIdAsync(id);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }
            return Ok(usuario);
        }

        /// <summary>
        /// Obtém um usuário pelo seu e-mail.
        /// </summary>

        [HttpGet("por-email")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> ObterUsuarioPorEmail([FromQuery] string email)
        {
            var usuario = await _userService.ObterUsuarioPorEmailAsync(email);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }
            return Ok(usuario);
        }

        /// <summary>
        /// Verifica se um usuário existe com um e-mail específico.
        /// </summary>

        [HttpGet("existe-por-email")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<ActionResult<bool>> ExisteUsuarioPorEmail([FromQuery] string email)
        {
            var existe = await _userService.ExisteUsuarioPorEmailAsync(email);
            return Ok(existe);
        }

        /// <summary>
        /// Adiciona um novo usuário (com hash de senha).
        /// </summary>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AdicionarUsuario([FromBody] SalvarUserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verificação de e-mail duplicado antes de chamar o serviço
            if (await _userService.ExisteUsuarioPorEmailAsync(userDto.Email))
            {
                ModelState.AddModelError(nameof(SalvarUserDto.Email), "Este e-mail já está em uso.");
                return BadRequest(ModelState);
            }

            try
            {
                var novoUsuario = await _userService.AdicionarUsuarioAsync(userDto);
                return CreatedAtAction(nameof(ObterUsuarioPorId), new { id = novoUsuario.Id }, novoUsuario);
            }
            catch (ArgumentException ex)
            {
                // Captura exceções de validação do serviço (ex: senha em branco)
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza um usuário (e opcionalmente sua senha).
        /// </summary>

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AtualizarUsuario(int id, [FromBody] SalvarUserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sucesso = await _userService.AtualizarUsuarioAsync(id, userDto);
            if (!sucesso)
            {
                return NotFound("Usuário não encontrado para atualização.");
            }

            return NoContent();
        }

        /// <summary>
        /// Exclui um usuário.
        /// </summary>

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ExcluirUsuario(int id)
        {
            var sucesso = await _userService.ExcluirUsuarioAsync(id);
            if (!sucesso)
            {
                return NotFound("Usuário não encontrado para exclusão.");
            }

            return NoContent();
        }
    }
}