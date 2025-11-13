using Microsoft.AspNetCore.Mvc;
using Skauts.DTOs;
using Skauts.Services.Interfaces;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace Skauts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IUsersOrganizationService _usersOrgService;

        public AuthController(IUserService userService, ITokenService tokenService,
            IUsersOrganizationService usersOrgService)
        {
            _userService = userService;
            _tokenService = tokenService;
            _usersOrgService = usersOrgService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            var user = await _userService.AutenticarAsync(loginDto.Email, loginDto.Password);

            if (user == null)
            {
                return Unauthorized("Email ou senha inválidos.");
            }

            var baseToken = await _tokenService.GerarTokenAsync(user, null);
            var organizacoes = await _usersOrgService.ObterOrganizacoesDoUsuarioAsync(user.Id);
            var token = await _tokenService.GerarTokenAsync(user);

            return Ok(new
            {
                token = baseToken,
                organizations = organizacoes
            });
        }

        [Authorize]
        [HttpPost("select-organization/{orgId}")]
        public async Task<IActionResult> SelectOrganization(int orgId)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out var userId))
            {
                return Unauthorized();
            }

            var userOrgs = await _usersOrgService.ObterOrganizacoesDoUsuarioAsync(userId);
            if (!userOrgs.Any(o => o.OrgId == orgId))
            {
                return Forbid("Usuário não tem permissão para acessar esta organização.");
            }

            var user = await _userService.ObterUsuarioCompletoPorIdAsync(userId);

            if (user == null)
            {
                return Unauthorized();
            }

            var finalToken = await _tokenService.GerarTokenAsync(user, orgId);

            return Ok(new { token = finalToken });
        }
    }
}