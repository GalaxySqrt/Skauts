using Skauts.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skauts.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço da relação N:N UsersOrganization
    /// </summary>
    public interface IUsersOrganizationService
    {
        Task<UsersOrganizationDto> ObterRelacaoAsync(int userId, int orgId);
        Task<List<UsersOrganizationDto>> ObterUsuariosDaOrganizacaoAsync(int orgId);
        Task<List<UsersOrganizationDto>> ObterOrganizacoesDoUsuarioAsync(int userId);
        Task<UsersOrganizationDto> AdicionarUsuarioNaOrganizacaoAsync(UsersOrganizationDto usersOrgDto);
        Task<bool> AtualizarRelacaoUsuarioOrganizacaoAsync(UsersOrganizationDto usersOrgDto); // Ex: para mudar 'Admin'
        Task<bool> RemoverUsuarioDaOrganizacaoAsync(int userId, int orgId);
    }
}