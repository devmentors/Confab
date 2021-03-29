using System;
using System.Threading.Tasks;

namespace Confab.Shared.Abstractions.Modules
{
    public interface IModuleSubscriber
    {
        IModuleSubscriber Subscribe<TRequest, TResponse>(string path,
            Func<TRequest, IServiceProvider, Task<TResponse>> action)
            where TRequest : class where TResponse : class;
    }
}