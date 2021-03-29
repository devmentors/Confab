using System;
using System.Threading.Tasks;
using Confab.Modules.Users.Core.Entities;

namespace Confab.Modules.Users.Core.Repositories
{
    internal interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
    }
}