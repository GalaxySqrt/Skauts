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
    public class PlayerService : IPlayerService
    {
        private readonly SkautsDbContext _context;
        private readonly IMapper _mapper;

        public PlayerService(SkautsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PlayerDto> AdicionarPlayerAsync(SalvarPlayerDto playerDto)
        {
            var player = _mapper.Map<Player>(playerDto);
            player.CreatedAt = DateTime.UtcNow; // Definindo data de criação

            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            return _mapper.Map<PlayerDto>(player);
        }

        public async Task<bool> AtualizarPlayerAsync(int id, SalvarPlayerDto playerDto)
        {
            var existing = await _context.Players.FindAsync(id);
            if (existing == null)
            {
                return false;
            }

            _mapper.Map(playerDto, existing); // Atualiza os campos da entidade
            _context.Players.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExcluirPlayerAsync(int id)
        {
            var existing = await _context.Players.FindAsync(id);
            if (existing == null)
            {
                return false;
            }

            _context.Players.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistePlayerPorEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return await _context.Players
                .AnyAsync(p => p.Email.ToLower() == email.ToLower());
        }

        public async Task<PlayerDto> ObterPlayerPorEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            return await _context.Players
                .AsNoTracking()
                .Where(p => p.Email.ToLower() == email.ToLower())
                .ProjectTo<PlayerDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<PlayerDto> ObterPlayerPorIdAsync(int id)
        {
            return await _context.Players
                .AsNoTracking()
                .Where(p => p.Id == id)
                .ProjectTo<PlayerDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<List<PlayerDto>> ObterPlayersAsync()
        {
            return await _context.Players
                .AsNoTracking()
                .ProjectTo<PlayerDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<PlayerDto>> ObterPlayersPorOrgAsync(int orgId)
        {
            return await _context.Players
                .AsNoTracking()
                .Where(p => p.OrgId == orgId)
                .ProjectTo<PlayerDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}