using Confab.Shared.Abstractions.Contexts;

namespace Confab.Shared.Infrastructure.Contexts
{
    internal interface IContextFactory
    {
        IContext Create();
    }
}