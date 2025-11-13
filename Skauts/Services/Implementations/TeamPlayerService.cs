using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Skauts.Data.Context;
using Skauts.DTOs;
using Skauts.Models;
using Skauts.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skauts.Services
{
    public class TeamPlayerService : ITeamPlayerService
    {
        private readonly SkautsDbContext _context;
        private readonly IMapper _mapper;

        public TeamPlayerService(SkautsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TeamPlayerDto> AdicionarJogadorAoTimeAsync(TeamPlayerDto teamPlayerDto)
        {
            // Verifica se a relação já existe
            var existing = await _context.TeamPlayers
                .FindAsync(teamPlayerDto.TeamId, teamPlayerDto.PlayerId);
            if (existing != null)
            {
                // Ou pode lançar uma exceção, dependendo da sua regra de negócio
                return _mapper.Map<TeamPlayerDto>(existing);
            }

            var teamPlayer = _mapper.Map<TeamPlayer>(teamPlayerDto);
            _context.TeamPlayers.Add(teamPlayer);
            await _context.SaveChangesAsync();
            return _mapper.Map<TeamPlayerDto>(teamPlayer);
        }

        public async Task<bool> AtualizarRelacaoAsync(TeamPlayerDto teamPlayerDto)
        {
            var existing = await _context.TeamPlayers
                .FindAsync(teamPlayerDto.TeamId, teamPlayerDto.PlayerId);
            if (existing == null)
            {
                return false;
            }

            // O mapeamento aqui atualiza propriedades extras (ex: IsCaptain, Number)
            _mapper.Map(teamPlayerDto, existing);
            _context.TeamPlayers.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoverJogadorDoTimeAsync(Guid teamId, int playerId)
        {
            var existing = await _context.TeamPlayers.FindAsync(teamId, playerId);
            if (existing == null)
            {
                return false;
            }
            _context.TeamPlayers.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<TeamPlayerDto>> ObterJogadoresDoTimeAsync(Guid teamId)
        {
            return await _context.TeamPlayers
                .AsNoTracking()
                .Where(tp => tp.TeamId == teamId)
                .ProjectTo<TeamPlayerDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<TeamPlayerDto> ObterRelacaoAsync(Guid teamId, int playerId)
        {
            return await _context.TeamPlayers
                .AsNoTracking()
                .Where(tp => tp.TeamId == teamId && tp.PlayerId == playerId)
                .ProjectTo<TeamPlayerDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<List<TeamPlayerDto>> ObterTimesDoJogadorAsync(int playerId)
        {
            return await _context.TeamPlayers
                .AsNoTracking()
                .Where(tp => tp.PlayerId == playerId)
                .ProjectTo<TeamPlayerDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}