using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Attendances.Domain.Exceptions
{
    public class AttendableEventNotFoundException : ConfabException
    {
        public Guid Id { get; }

        public AttendableEventNotFoundException(Guid id) 
            : base($"Attendable event with ID: '{id}' was not found.")
        {
            Id = id;
        }
    }
}