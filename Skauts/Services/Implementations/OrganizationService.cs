using AutoMapper;
using AutoMapper.QueryableExtensions; // Para o .ProjectTo()
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
    public class OrganizationService : IOrganizationService
    {
        private readonly SkautsDbContext _context;
        private readonly IMapper _mapper;

        public OrganizationService(SkautsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrganizationDto> AdicionarOrganizacaoAsync(SalvarOrganizationDto orgDto)
        {
            var organization = _mapper.Map<Organization>(orgDto);
            organization.CreatedAt = DateTime.UtcNow;

            _context.Organizations.Add(organization);
            await _context.SaveChangesAsync();

            return _mapper.Map<OrganizationDto>(organization);
        }

        public async Task<bool> AtualizarOrganizacaoAsync(int id, SalvarOrganizationDto orgDto)
        {
            var existingOrg = await _context.Organizations.FindAsync(id);
            if (existingOrg == null)
            {
                return false;
            }

            _mapper.Map(orgDto, existingOrg);

            _context.Organizations.Update(existingOrg);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExcluirOrganizacaoAsync(int id)
        {
            var existingOrg = await _context.Organizations.FindAsync(id);
            if (existingOrg == null)
            {
                return false;
            }

            _context.Organizations.Remove(existingOrg);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<OrganizationDto> ObterOrganizacaoPorIdAsync(int id)
        {
            return await _context.Organizations
                .AsNoTracking()
                .Where(o => o.Id == id)
                .ProjectTo<OrganizationDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<OrganizationDto> ObterOrganizacaoPorNomeAsync(string nome)
        {
            return await _context.Organizations
                .AsNoTracking()
                .Where(o => o.Name.Equals(nome, StringComparison.OrdinalIgnoreCase))
                .ProjectTo<OrganizationDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<List<OrganizationDto>> ObterOrganizacoesAsync()
        {
            return await _context.Organizations
                .AsNoTracking()
                .ProjectTo<OrganizationDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}