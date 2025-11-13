using Skauts.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skauts.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço da relação N:N TeamPlayer
    /// </summary>
    public interface ITeamPlayerService
    {
        Task<TeamPlayerDto> ObterRelacaoAsync(Guid teamId, int playerId);
        Task<List<TeamPlayerDto>> ObterJogadoresDoTimeAsync(Guid teamId);
        Task<List<TeamPlayerDto>> ObterTimesDoJogadorAsync(int playerId);
        Task<TeamPlayerDto> AdicionarJogadorAoTimeAsync(TeamPlayerDto teamPlayerDto);
        Task<bool> AtualizarRelacaoAsync(TeamPlayerDto teamPlayerDto);
        Task<bool> RemoverJogadorDoTimeAsync(Guid teamId, int playerId);
    }
}