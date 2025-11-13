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
    public class PlayersPrizeService : IPlayersPrizeService
    {
        private readonly SkautsDbContext _context;
        private readonly IMapper _mapper;

        public PlayersPrizeService(SkautsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PlayersPrizeDto> AdicionarPremioDeJogadorAsync(SalvarPlayersPrizeDto prizeDto)
        {
            var prize = _mapper.Map<PlayersPrize>(prizeDto);
            _context.PlayersPrizes.Add(prize);
            await _context.SaveChangesAsync();
            return _mapper.Map<PlayersPrizeDto>(prize);
        }

        public async Task<bool> AtualizarPremioDeJogadorAsync(int id, SalvarPlayersPrizeDto prizeDto)
        {
            var existing = await _context.PlayersPrizes.FindAsync(id);
            if (existing == null)
            {
                return false;
            }
            _mapper.Map(prizeDto, existing);
            _context.PlayersPrizes.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExcluirPremioDeJogadorAsync(int id)
        {
            var existing = await _context.PlayersPrizes.FindAsync(id);
            if (existing == null)
            {
                return false;
            }
            _context.PlayersPrizes.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PlayersPrizeDto> ObterPremioDeJogadorPorIdAsync(int id)
        {
            return await _context.PlayersPrizes
                .AsNoTracking()
                .Where(pp => pp.Id == id)
                .ProjectTo<PlayersPrizeDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<List<PlayersPrizeDto>> ObterPremiosDeJogadoresAsync()
        {
            return await _context.PlayersPrizes
                .AsNoTracking()
                .ProjectTo<PlayersPrizeDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<PlayersPrizeDto>> ObterPremiosPorJogadorAsync(int playerId)
        {
            return await _context.PlayersPrizes
                .AsNoTracking()
                .Where(pp => pp.PlayerId == playerId)
                .ProjectTo<PlayersPrizeDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<PlayersPrizeDto>> ObterPremiosPorTipoDePremioAsync(int prizeTypeId)
        {
            return await _context.PlayersPrizes
                .AsNoTracking()
                .Where(pp => pp.PrizeTypeId == prizeTypeId)
                .ProjectTo<PlayersPrizeDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}