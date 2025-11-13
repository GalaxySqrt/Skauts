using Skauts.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skauts.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço de Championship
    /// </summary>
    public interface IChampionshipService
    {
        Task<List<ChampionshipDto>> ObterCampeonatosAsync();
        Task<ChampionshipDto> ObterCampeonatoPorIdAsync(int id);
        Task<List<ChampionshipDto>> ObterCampeonatosPorOrgAsync(int orgId);
        Task<ChampionshipDto> ObterCampeonatoPorNomeAsync(string nome);
        Task<ChampionshipDto> AdicionarCampeonatoAsync(SalvarChampionshipDto championshipDto);
        Task<bool> AtualizarCampeonatoAsync(int id, SalvarChampionshipDto championshipDto);
        Task<bool> ExcluirCampeonatoAsync(int id);
    }
}