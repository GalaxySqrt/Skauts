using Skauts.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skauts.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço de Match
    /// </summary>
    public interface IMatchService
    {
        Task<List<MatchDto>> ObterMatchesAsync();
        Task<MatchDto> ObterMatchPorIdAsync(int id);
        Task<List<MatchDto>> ObterMatchesPorOrgAsync(int orgId);
        Task<List<MatchDto>> ObterMatchesPorTimeAsync(Guid teamId);
        Task<List<MatchDto>> ObterMatchesPorCampeonatoAsync(int championshipId);
        Task<List<MatchDto>> ObterMatchesPorDataAsync(DateTime data);
        Task<MatchDto> AdicionarMatchAsync(SalvarMatchDto matchDto);
        Task<bool> AtualizarMatchAsync(int id, SalvarMatchDto matchDto);
        Task<bool> ExcluirMatchAsync(int id);
    }
}