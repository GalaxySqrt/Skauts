using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Skauts.Data.Context;
using Skauts.DTOs;
using Skauts.Models;
using Skauts.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skauts.Services
{
    public class ChampionshipService : IChampionshipService
    {
        private readonly SkautsDbContext _context;
        private readonly IMapper _mapper;

        public ChampionshipService(SkautsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ChampionshipDto> AdicionarCampeonatoAsync(SalvarChampionshipDto championshipDto)
        {
            var championship = _mapper.Map<Championship>(championshipDto);

            _context.Championships.Add(championship);
            await _context.SaveChangesAsync();

            return _mapper.Map<ChampionshipDto>(championship);
        }

        public async Task<bool> AtualizarCampeonatoAsync(int id, SalvarChampionshipDto championshipDto)
        {
            var existing = await _context.Championships.FindAsync(id);
            if (existing == null)
            {
                return false;
            }

            _mapper.Map(championshipDto, existing);
            _context.Championships.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExcluirCampeonatoAsync(int id)
        {
            var existing = await _context.Championships.FindAsync(id);
            if (existing == null)
            {
                return false;
            }

            _context.Championships.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ChampionshipDto> ObterCampeonatoPorIdAsync(int id)
        {
            return await _context.Championships
                .AsNoTracking()
                .Where(c => c.Id == id)
                .ProjectTo<ChampionshipDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<ChampionshipDto> ObterCampeonatoPorNomeAsync(string nome)
        {
            return await _context.Championships
                .AsNoTracking()
                .Where(c => c.Name.Equals(nome, StringComparison.OrdinalIgnoreCase))
                .ProjectTo<ChampionshipDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<List<ChampionshipDto>> ObterCampeonatosAsync()
        {
            return await _context.Championships
                .AsNoTracking()
                .ProjectTo<ChampionshipDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<ChampionshipDto>> ObterCampeonatosPorOrgAsync(int orgId)
        {
            return await _context.Championships
                .AsNoTracking()
                .Where(c => c.OrgId == orgId)
                .ProjectTo<ChampionshipDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}