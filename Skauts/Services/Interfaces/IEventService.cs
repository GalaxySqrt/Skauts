using Skauts.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skauts.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço de Event (Evento da partida)
    /// </summary>
    public interface IEventService
    {
        Task<List<EventDto>> ObterEventosAsync();
        Task<EventDto> ObterEventoPorIdAsync(int id);
        Task<List<EventDto>> ObterEventosPorPartidaAsync(int matchId);
        Task<List<EventDto>> ObterEventosPorJogadorAsync(int playerId);
        Task<List<EventDto>> ObterEventosPorTipoAsync(int eventTypeId);
        Task<EventDto> AdicionarEventoAsync(SalvarEventDto eventDto);
        Task<bool> AtualizarEventoAsync(int id, SalvarEventDto eventDto);
        Task<bool> ExcluirEventoAsync(int id);
    }
}