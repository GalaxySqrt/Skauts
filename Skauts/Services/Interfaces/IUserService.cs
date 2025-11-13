using Skauts.DTOs;
using Skauts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skauts.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço de User
    /// </summary>
    public interface IUserService
    {
        Task<User> AutenticarAsync(string email, string senha);
        Task<List<UserDto>> ObterUsuariosAsync();
        Task<UserDto> ObterUsuarioPorIdAsync(int id);
        Task<User> ObterUsuarioCompletoPorIdAsync(int id);
        Task<UserDto> ObterUsuarioPorEmailAsync(string email);
        Task<bool> ExisteUsuarioPorEmailAsync(string email);
        Task<UserDto> AdicionarUsuarioAsync(SalvarUserDto userDto);
        Task<bool> AtualizarUsuarioAsync(int id, SalvarUserDto userDto);
        Task<bool> ExcluirUsuarioAsync(int id);
    }
}