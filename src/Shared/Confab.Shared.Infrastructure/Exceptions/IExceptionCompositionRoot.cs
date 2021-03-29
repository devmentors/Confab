using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Shared.Infrastructure.Exceptions
{
    internal interface IExceptionCompositionRoot
    {
        ExceptionResponse Map(Exception exception);
    }
}