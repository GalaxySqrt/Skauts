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
    public class EventService : IEventService
    {
        private readonly SkautsDbContext _context;
        private readonly IMapper _mapper;

        public EventService(SkautsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EventDto> AdicionarEventoAsync(SalvarEventDto eventDto)
        {
            var newEvent = _mapper.Map<Event>(eventDto);

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            return _mapper.Map<EventDto>(newEvent);
        }

        public async Task<bool> AtualizarEventoAsync(int id, SalvarEventDto eventDto)
        {
            var existing = await _context.Events.FindAsync(id);
            if (existing == null)
            {
                return false;
            }

            _mapper.Map(eventDto, existing);
            _context.Events.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExcluirEventoAsync(int id)
        {
            var existing = await _context.Events.FindAsync(id);
            if (existing == null)
            {
                return false;
            }

            _context.Events.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<EventDto> ObterEventoPorIdAsync(int id)
        {
            return await _context.Events
                .AsNoTracking()
                .Where(e => e.Id == id)
                .ProjectTo<EventDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<List<EventDto>> ObterEventosAsync()
        {
            return await _context.Events
                .AsNoTracking()
                .ProjectTo<EventDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<EventDto>> ObterEventosPorJogadorAsync(int playerId)
        {
            return await _context.Events
                .AsNoTracking()
                .Where(e => e.PlayerId == playerId)
                .ProjectTo<EventDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<EventDto>> ObterEventosPorPartidaAsync(int matchId)
        {
            return await _context.Events
                .AsNoTracking()
                .Where(e => e.MatchId == matchId)
                .ProjectTo<EventDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<EventDto>> ObterEventosPorTipoAsync(int eventTypeId)
        {
            return await _context.Events
                .AsNoTracking()
                .Where(e => e.EventTypeId == eventTypeId)
                .ProjectTo<EventDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}