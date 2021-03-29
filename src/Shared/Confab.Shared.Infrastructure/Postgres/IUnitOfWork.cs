using System;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Postgres
{
    public interface IUnitOfWork
    {
        Task ExecuteAsync(Func<Task> action);
    }
}