namespace Confab.Shared.Abstractions.Contexts
{
    public interface IContext
    {
        string RequestId { get; }
        string TraceId { get; }
        IIdentityContext Identity { get; }
    }
}