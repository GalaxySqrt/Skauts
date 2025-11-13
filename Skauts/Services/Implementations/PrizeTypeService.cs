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
    public class PrizeTypeService : IPrizeTypeService
    {
        private readonly SkautsDbContext _context;
        private readonly IMapper _mapper;

        public PrizeTypeService(SkautsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PrizeTypeDto> AdicionarTipoDePremioAsync(SalvarPrizeTypeDto prizeTypeDto)
        {
            var prizeType = _mapper.Map<PrizeType>(prizeTypeDto);
            _context.PrizeTypes.Add(prizeType);
            await _context.SaveChangesAsync();
            return _mapper.Map<PrizeTypeDto>(prizeType);
        }

        public async Task<bool> AtualizarTipoDePremioAsync(int id, SalvarPrizeTypeDto prizeTypeDto)
        {
            var existing = await _context.PrizeTypes.FindAsync(id);
            if (existing == null)
            {
                return false;
            }
            _mapper.Map(prizeTypeDto, existing);
            _context.PrizeTypes.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExcluirTipoDePremioAsync(int id)
        {
            var existing = await _context.PrizeTypes.FindAsync(id);
            if (existing == null)
            {
                return false;
            }
            _context.PrizeTypes.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PrizeTypeDto> ObterTipoDePremioPorIdAsync(int id)
        {
            return await _context.PrizeTypes
                .AsNoTracking()
                .Where(pt => pt.Id == id)
                .ProjectTo<PrizeTypeDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<PrizeTypeDto> ObterTipoDePremioPorNomeAsync(string nome)
        {
            return await _context.PrizeTypes
                .AsNoTracking()
                .Where(pt => pt.Name.ToLower() == nome.ToLower())
                .ProjectTo<PrizeTypeDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<List<PrizeTypeDto>> ObterTiposDePremioAsync()
        {
            return await _context.PrizeTypes
                .AsNoTracking()
                .ProjectTo<PrizeTypeDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}