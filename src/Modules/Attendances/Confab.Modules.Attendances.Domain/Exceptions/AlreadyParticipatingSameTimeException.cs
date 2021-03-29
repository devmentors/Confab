using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Attendances.Domain.Exceptions
{
    public class AlreadyParticipatingSameTimeException : ConfabException
    {
        public AlreadyParticipatingSameTimeException() : base("Already participating in the same time.")
        {
        }
    }
}