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
    public class RoleService : IRoleService
    {
        private readonly SkautsDbContext _context;
        private readonly IMapper _mapper;

        public RoleService(SkautsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RoleDto> AdicionarRoleAsync(SalvarRoleDto roleDto)
        {
            var role = _mapper.Map<Role>(roleDto);
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return _mapper.Map<RoleDto>(role);
        }

        public async Task<bool> AtualizarRoleAsync(int id, SalvarRoleDto roleDto)
        {
            var existing = await _context.Roles.FindAsync(id);
            if (existing == null)
            {
                return false;
            }
            _mapper.Map(roleDto, existing);
            _context.Roles.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExcluirRoleAsync(int id)
        {
            var existing = await _context.Roles.FindAsync(id);
            if (existing == null)
            {
                return false;
            }
            _context.Roles.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<RoleDto> ObterRolePorAcronimoAsync(string acronimo)
        {
            return await _context.Roles
                .AsNoTracking()
                .Where(r => r.Acronym.ToLower() == acronimo.ToLower())
                .ProjectTo<RoleDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<RoleDto> ObterRolePorIdAsync(int id)
        {
            return await _context.Roles
                .AsNoTracking()
                .Where(r => r.Id == id)
                .ProjectTo<RoleDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<RoleDto> ObterRolePorNomeAsync(string nome)
        {
            return await _context.Roles
                .AsNoTracking()
                .Where(r => r.Name.ToLower() == nome.ToLower())
                .ProjectTo<RoleDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<List<RoleDto>> ObterRolesAsync()
        {
            return await _context.Roles
                .AsNoTracking()
                .ProjectTo<RoleDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}