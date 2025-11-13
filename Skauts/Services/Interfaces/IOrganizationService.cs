using Skauts.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skauts.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço de Organization
    /// </summary>
    public interface IOrganizationService
    {
        Task<List<OrganizationDto>> ObterOrganizacoesAsync();
        Task<OrganizationDto> ObterOrganizacaoPorIdAsync(int id);
        Task<OrganizationDto> ObterOrganizacaoPorNomeAsync(string nome);
        Task<OrganizationDto> AdicionarOrganizacaoAsync(SalvarOrganizationDto orgDto);
        Task<bool> AtualizarOrganizacaoAsync(int id, SalvarOrganizationDto orgDto);
        Task<bool> ExcluirOrganizacaoAsync(int id);
    }
}