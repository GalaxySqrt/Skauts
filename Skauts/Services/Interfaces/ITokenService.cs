using Skauts.DTOs;
using Skauts.Models;
using System.Threading.Tasks;

namespace Skauts.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> GerarTokenAsync(User user, int? orgId = null);
    }
}