using System;

namespace Confab.Shared.Abstractions.Exceptions
{
    public abstract class ConfabException : Exception
    {
        protected ConfabException(string message) : base(message)
        {
        }
    }
}