using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Users.Core.Exceptions
{
    internal class InvalidCredentialsException : ConfabException
    {
        public InvalidCredentialsException() : base("Invalid credentials.")
        {
        }
    }
}