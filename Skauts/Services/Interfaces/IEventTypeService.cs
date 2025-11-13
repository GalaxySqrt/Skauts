using Skauts.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skauts.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço de EventType (Tipo de Evento)
    /// </summary>
    public interface IEventTypeService
    {
        Task<List<EventTypeDto>> ObterTiposDeEventoAsync();
        Task<EventTypeDto> ObterTipoDeEventoPorIdAsync(int id);
        Task<EventTypeDto> ObterTipoDeEventoPorNomeAsync(string nome);
        Task<EventTypeDto> AdicionarTipoDeEventoAsync(SalvarEventTypeDto eventTypeDto);
        Task<bool> AtualizarTipoDeEventoAsync(int id, SalvarEventTypeDto eventTypeDto);
        Task<bool> ExcluirTipoDeEventoAsync(int id);
    }
}