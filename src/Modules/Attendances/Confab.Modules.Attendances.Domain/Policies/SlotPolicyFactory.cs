using System.Linq;

namespace Confab.Modules.Attendances.Domain.Policies
{
    public class SlotPolicyFactory : ISlotPolicyFactory
    {
        public ISlotPolicy Get(params string[] tags)
            => tags switch
            {
                { } when tags.Contains("stationary") => new RegularSlotPolicy(),
                { } when tags.Contains("workshops") => new RegularSlotPolicy(),
                _ => new OverbookingSlotPolicy()
            };
    }
}