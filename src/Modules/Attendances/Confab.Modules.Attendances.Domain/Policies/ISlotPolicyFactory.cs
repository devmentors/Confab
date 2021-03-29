namespace Confab.Modules.Attendances.Domain.Policies
{
    public interface ISlotPolicyFactory
    {
        ISlotPolicy Get(params string[] tags);
    }
}