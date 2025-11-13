using Skauts.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skauts.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço de Role (Posição/Função)
    /// </summary>
    public interface IRoleService
    {
        Task<List<RoleDto>> ObterRolesAsync();
        Task<RoleDto> ObterRolePorIdAsync(int id);
        Task<RoleDto> ObterRolePorNomeAsync(string nome);
        Task<RoleDto> ObterRolePorAcronimoAsync(string acronimo);
        Task<RoleDto> AdicionarRoleAsync(SalvarRoleDto roleDto);
        Task<bool> AtualizarRoleAsync(int id, SalvarRoleDto roleDto);
        Task<bool> ExcluirRoleAsync(int id);
    }
}