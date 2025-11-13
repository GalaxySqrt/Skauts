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
    public class EventTypeService : IEventTypeService
    {
        private readonly SkautsDbContext _context;
        private readonly IMapper _mapper;

        public EventTypeService(SkautsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EventTypeDto> AdicionarTipoDeEventoAsync(SalvarEventTypeDto eventTypeDto)
        {
            var eventType = _mapper.Map<EventType>(eventTypeDto);

            _context.EventTypes.Add(eventType);
            await _context.SaveChangesAsync();

            return _mapper.Map<EventTypeDto>(eventType);
        }

        public async Task<bool> AtualizarTipoDeEventoAsync(int id, SalvarEventTypeDto eventTypeDto)
        {
            var existing = await _context.EventTypes.FindAsync(id);
            if (existing == null)
            {
                return false;
            }

            _mapper.Map(eventTypeDto, existing);
            _context.EventTypes.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExcluirTipoDeEventoAsync(int id)
        {
            var existing = await _context.EventTypes.FindAsync(id);
            if (existing == null)
            {
                return false;
            }

            _context.EventTypes.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<EventTypeDto> ObterTipoDeEventoPorIdAsync(int id)
        {
            return await _context.EventTypes
                .AsNoTracking()
                .Where(e => e.Id == id)
                .ProjectTo<EventTypeDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<EventTypeDto> ObterTipoDeEventoPorNomeAsync(string nome)
        {
            return await _context.EventTypes
                .AsNoTracking()
                .Where(e => e.Name.Equals(nome, StringComparison.OrdinalIgnoreCase))
                .ProjectTo<EventTypeDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<List<EventTypeDto>> ObterTiposDeEventoAsync()
        {
            return await _context.EventTypes
                .AsNoTracking()
                .ProjectTo<EventTypeDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}