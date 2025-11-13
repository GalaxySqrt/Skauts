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
using BCrypt.Net;

namespace Skauts.Services
{
    public class UserService : IUserService
    {
        private readonly SkautsDbContext _context;
        private readonly IMapper _mapper;

        public UserService(SkautsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private string GerarHashDaSenha(string senha)
        {
            int costFactor = 12;
            return BCrypt.Net.BCrypt.HashPassword(senha, costFactor);
        }

        public async Task<User> AutenticarAsync(string email, string senha)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

            if (user == null)
            {
                return null; // Usuário não encontrado
            }

            if (!BCrypt.Net.BCrypt.Verify(senha, user.PasswordHash))
            {
                return null; // Senha inválida
            }

            return user; // Sucesso
        }
        public async Task<UserDto> AdicionarUsuarioAsync(SalvarUserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            if (string.IsNullOrWhiteSpace(userDto.Password))
            {
                throw new ArgumentException("A senha é obrigatória...", nameof(userDto));
            }
            user.PasswordHash = GerarHashDaSenha(userDto.Password);

            user.CreatedAt = DateTime.UtcNow;
            _context.Users.Add(user);

            await _context.SaveChangesAsync();
            return _mapper.Map<UserDto>(user);
        }

        public async Task<bool> AtualizarUsuarioAsync(int id, SalvarUserDto userDto)
        {
            var existing = await _context.Users.FindAsync(id);
            if (existing == null)
            {
                return false;
            }

            _mapper.Map(userDto, existing);

            if (!string.IsNullOrWhiteSpace(userDto.Password))
            {
                existing.PasswordHash = GerarHashDaSenha(userDto.Password);
            }
            // ---------------------------------------------------

            _context.Users.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExcluirUsuarioAsync(int id)
        {
            var existing = await _context.Users.FindAsync(id);
            if (existing == null)
            {
                return false;
            }
            _context.Users.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExisteUsuarioPorEmailAsync(string email)
        {
            return await _context.Users
                .AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<UserDto> ObterUsuarioPorEmailAsync(string email)
        {
            return await _context.Users
                .AsNoTracking()
                .Where(u => u.Email.ToLower() == email.ToLower())
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<UserDto> ObterUsuarioPorIdAsync(int id)
        {
            return await _context.Users
                .AsNoTracking()
                .Where(u => u.Id == id)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<List<UserDto>> ObterUsuariosAsync()
        {
            return await _context.Users
                .AsNoTracking()
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
        public async Task<User> ObterUsuarioCompletoPorIdAsync(int id)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}