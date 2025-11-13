using Skauts.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skauts.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço de PlayersPrize (Prêmios dos Jogadores)
    /// </summary>
    public interface IPlayersPrizeService
    {
        Task<List<PlayersPrizeDto>> ObterPremiosDeJogadoresAsync();
        Task<PlayersPrizeDto> ObterPremioDeJogadorPorIdAsync(int id);
        Task<List<PlayersPrizeDto>> ObterPremiosPorJogadorAsync(int playerId);
        Task<List<PlayersPrizeDto>> ObterPremiosPorTipoDePremioAsync(int prizeTypeId);
        Task<PlayersPrizeDto> AdicionarPremioDeJogadorAsync(SalvarPlayersPrizeDto prizeDto);
        Task<bool> AtualizarPremioDeJogadorAsync(int id, SalvarPlayersPrizeDto prizeDto);
        Task<bool> ExcluirPremioDeJogadorAsync(int id);
    }
}