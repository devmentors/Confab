using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Attendances.Domain.Exceptions
{
    public class NoFreeSlotsException : ConfabException
    {
        public NoFreeSlotsException() : base("No free slots left.")
        {
        }
    }
}