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
    public class UsersOrganizationService : IUsersOrganizationService
    {
        private readonly SkautsDbContext _context;
        private readonly IMapper _mapper;

        public UsersOrganizationService(SkautsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UsersOrganizationDto> AdicionarUsuarioNaOrganizacaoAsync(UsersOrganizationDto usersOrgDto)
        {
            // Verifica se a relação já existe
            var existing = await _context.UsersOrganizations
                .FindAsync(usersOrgDto.UserId, usersOrgDto.OrgId);

            if (existing != null)
            {
                // Decide se lança exceção ou só retorna o existente
                return _mapper.Map<UsersOrganizationDto>(existing);
            }

            var relation = _mapper.Map<UsersOrganization>(usersOrgDto);
            _context.UsersOrganizations.Add(relation);
            await _context.SaveChangesAsync();
            return _mapper.Map<UsersOrganizationDto>(relation);
        }

        public async Task<bool> AtualizarRelacaoUsuarioOrganizacaoAsync(UsersOrganizationDto usersOrgDto)
        {
            var existing = await _context.UsersOrganizations
                .FindAsync(usersOrgDto.UserId, usersOrgDto.OrgId);

            if (existing == null)
            {
                return false;
            }

            // Atualiza campos (ex: a flag 'Admin')
            _mapper.Map(usersOrgDto, existing);
            _context.UsersOrganizations.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoverUsuarioDaOrganizacaoAsync(int userId, int orgId)
        {
            var existing = await _context.UsersOrganizations.FindAsync(userId, orgId);
            if (existing == null)
            {
                return false;
            }
            _context.UsersOrganizations.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<UsersOrganizationDto>> ObterOrganizacoesDoUsuarioAsync(int userId)
        {
            return await _context.UsersOrganizations
                .AsNoTracking()
                .Where(uo => uo.UserId == userId)
                .ProjectTo<UsersOrganizationDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<UsersOrganizationDto> ObterRelacaoAsync(int userId, int orgId)
        {
            return await _context.UsersOrganizations
                .AsNoTracking()
                .Where(uo => uo.UserId == userId && uo.OrgId == orgId)
                .ProjectTo<UsersOrganizationDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<List<UsersOrganizationDto>> ObterUsuariosDaOrganizacaoAsync(int orgId)
        {
            return await _context.UsersOrganizations
                .AsNoTracking()
                .Where(uo => uo.OrgId == orgId)
                .ProjectTo<UsersOrganizationDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}