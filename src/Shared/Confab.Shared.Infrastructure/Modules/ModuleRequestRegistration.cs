using System;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Modules
{
    public sealed class ModuleRequestRegistration
    {
        public Type RequestType { get; }
        public Type ResponseType { get; }
        public Func<object, Task<object>> Action { get; }

        public ModuleRequestRegistration(Type requestType, Type responseType, Func<object, Task<object>> action)
        {
            RequestType = requestType;
            ResponseType = responseType;
            Action = action;
        }
    }
}