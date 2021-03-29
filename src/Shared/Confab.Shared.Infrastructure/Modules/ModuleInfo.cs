using System.Collections.Generic;

namespace Confab.Shared.Infrastructure.Modules
{
    internal record ModuleInfo(string Name, string Path, IEnumerable<string> Policies);
}