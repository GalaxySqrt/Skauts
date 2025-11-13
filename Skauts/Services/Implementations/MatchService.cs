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
    public class MatchService : IMatchService
    {
        private readonly SkautsDbContext _context;
        private readonly IMapper _mapper;

        public MatchService(SkautsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MatchDto> AdicionarMatchAsync(SalvarMatchDto matchDto)
        {
            var match = _mapper.Map<Match>(matchDto);

            _context.Matches.Add(match);
            await _context.SaveChangesAsync();

            return _mapper.Map<MatchDto>(match);
        }

        public async Task<bool> AtualizarMatchAsync(int id, SalvarMatchDto matchDto)
        {
            var existing = await _context.Matches.FindAsync(id);
            if (existing == null)
            {
                return false;
            }

            _mapper.Map(matchDto, existing);
            _context.Matches.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExcluirMatchAsync(int id)
        {
            var existing = await _context.Matches.FindAsync(id);
            if (existing == null)
            {
                return false;
            }

            _context.Matches.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<MatchDto> ObterMatchPorIdAsync(int id)
        {
            return await _context.Matches
                .AsNoTracking()
                .Where(m => m.Id == id)
                .ProjectTo<MatchDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<List<MatchDto>> ObterMatchesAsync()
        {
            return await _context.Matches
                .AsNoTracking()
                .ProjectTo<MatchDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<MatchDto>> ObterMatchesPorCampeonatoAsync(int championshipId)
        {
            return await _context.Matches
                .AsNoTracking()
                .Where(m => m.ChampionshipId == championshipId)
                .ProjectTo<MatchDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<MatchDto>> ObterMatchesPorDataAsync(DateTime data)
        {
            return await _context.Matches
                .AsNoTracking()
                .Where(m => m.Date.Date == data.Date)
                .ProjectTo<MatchDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<MatchDto>> ObterMatchesPorOrgAsync(int orgId)
        {
            return await _context.Matches
                .AsNoTracking()
                .Where(m => m.OrgId == orgId)
                .ProjectTo<MatchDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<MatchDto>> ObterMatchesPorTimeAsync(Guid teamId)
        {
            return await _context.Matches
                .AsNoTracking()
                .Where(m => m.TeamAId == teamId || m.TeamBId == teamId)
                .ProjectTo<MatchDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}