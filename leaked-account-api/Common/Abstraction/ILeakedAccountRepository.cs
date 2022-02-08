using LeakedAccountApi.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeakedAccountApi.Common.Abstraction
{
    public interface ILeakedAccountRepository
    {
        Task<LeakedAccount> GetLeakedAccountByEmail(string email);

        Task<bool> CheckIsLeakedAccount(string email, string password);

        Task<bool> CreateLeakedAccount(LeakedAccount leakedAccount);

        Task<bool> UpdateLeakedAccount(List<string> passwords);

        Task<bool> DeleteLeakedAccount(string email);

    }
}
