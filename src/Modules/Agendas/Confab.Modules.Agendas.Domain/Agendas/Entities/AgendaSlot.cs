using System;
using Confab.Modules.Agendas.Domain.CallForPapers.Exceptions;
using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Modules.Agendas.Domain.Agendas.Entities
{
    public abstract class AgendaSlot
    {
        public EntityId Id { get; protected set; }
        public DateTime From { get; protected set;}
        public DateTime To { get; protected set;}
        
        public AgendaTrack Track { get; protected set; }

        protected AgendaSlot(EntityId id, DateTime from, DateTime to)
        {
            Id = id;
            From = from;
            To = to;
        }

        protected AgendaSlot()
        {
        }

        protected AgendaSlot(EntityId id)
            => Id = id;
        
        protected void ChangeDateRange(DateTime from, DateTime to)
        {
            if (from.Date > to.Date)
            {
                throw new InvalidCallForPapersDatesException(from, to);
            }

            From = from;
            To = to;
        }
    }
}