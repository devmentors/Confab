using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Modules
{
    public interface IModuleRegistry
    {
        IEnumerable<ModuleBroadcastRegistration> GetBroadcastRegistrations(string key); 
        ModuleRequestRegistration GetRequestRegistration(string path);
        void AddBroadcastAction(Type requestType, Func<object, Task> action);
        void AddRequestAction(string path, Type requestType, Type responseType, Func<object, Task<object>> action);
    }
}