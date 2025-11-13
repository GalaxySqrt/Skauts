using Skauts.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skauts.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço de Player
    /// </summary>
    public interface IPlayerService
    {
        Task<List<PlayerDto>> ObterPlayersAsync();
        Task<PlayerDto> ObterPlayerPorIdAsync(int id);
        Task<List<PlayerDto>> ObterPlayersPorOrgAsync(int orgId);
        Task<PlayerDto> ObterPlayerPorEmailAsync(string email);
        Task<bool> ExistePlayerPorEmailAsync(string email);
        Task<PlayerDto> AdicionarPlayerAsync(SalvarPlayerDto playerDto);
        Task<bool> AtualizarPlayerAsync(int id, SalvarPlayerDto playerDto);
        Task<bool> ExcluirPlayerAsync(int id);
    }
}