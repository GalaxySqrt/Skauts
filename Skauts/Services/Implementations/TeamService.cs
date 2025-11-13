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
    public class TeamService : ITeamService
    {
        private readonly SkautsDbContext _context;
        private readonly IMapper _mapper;

        public TeamService(SkautsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TeamDto> AdicionarTimeAsync(SalvarTeamDto teamDto)
        {
            var team = _mapper.Map<Team>(teamDto);

            // Assumindo que a entidade Team tem um 'CreatedAt' ou lógica similar
            // team.CreatedAt = DateTime.UtcNow; 

            _context.Teams.Add(team);
            await _context.SaveChangesAsync();
            return _mapper.Map<TeamDto>(team);
        }

        public async Task<bool> AtualizarTimeAsync(Guid id, SalvarTeamDto teamDto)
        {
            var existing = await _context.Teams.FindAsync(id);
            if (existing == null)
            {
                return false;
            }
            _mapper.Map(teamDto, existing);
            _context.Teams.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExcluirTimeAsync(Guid id)
        {
            var existing = await _context.Teams.FindAsync(id);
            if (existing == null)
            {
                return false;
            }
            _context.Teams.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TeamDto> ObterTimePorIdAsync(Guid id)
        {
            return await _context.Teams
                .AsNoTracking()
                .Where(t => t.Id == id)
                .ProjectTo<TeamDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<TeamDto> ObterTimePorNomeAsync(string nome)
        {
            return await _context.Teams
                .AsNoTracking()
                .Where(t => t.Name.ToLower() == nome.ToLower())
                .ProjectTo<TeamDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<List<TeamDto>> ObterTimesAsync()
        {
            return await _context.Teams
                .AsNoTracking()
                .ProjectTo<TeamDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<TeamDto>> ObterTimesPorOrgAsync(int orgId)
        {
            return await _context.Teams
                .AsNoTracking()
                .Where(t => t.OrgId == orgId)
                .ProjectTo<TeamDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}