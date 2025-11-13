using Skauts.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skauts.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço de PrizeType (Tipo de Prêmio)
    /// </summary>
    public interface IPrizeTypeService
    {
        Task<List<PrizeTypeDto>> ObterTiposDePremioAsync();
        Task<PrizeTypeDto> ObterTipoDePremioPorIdAsync(int id);
        Task<PrizeTypeDto> ObterTipoDePremioPorNomeAsync(string nome);
        Task<PrizeTypeDto> AdicionarTipoDePremioAsync(SalvarPrizeTypeDto prizeTypeDto);
        Task<bool> AtualizarTipoDePremioAsync(int id, SalvarPrizeTypeDto prizeTypeDto);
        Task<bool> ExcluirTipoDePremioAsync(int id);
    }
}