using Skauts.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skauts.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço de Team
    /// </summary>
    public interface ITeamService
    {
        Task<List<TeamDto>> ObterTimesAsync();
        Task<TeamDto> ObterTimePorIdAsync(Guid id);
        Task<List<TeamDto>> ObterTimesPorOrgAsync(int orgId);
        Task<TeamDto> ObterTimePorNomeAsync(string nome);
        Task<TeamDto> AdicionarTimeAsync(SalvarTeamDto teamDto);
        Task<bool> AtualizarTimeAsync(Guid id, SalvarTeamDto teamDto);
        Task<bool> ExcluirTimeAsync(Guid id);
    }
}