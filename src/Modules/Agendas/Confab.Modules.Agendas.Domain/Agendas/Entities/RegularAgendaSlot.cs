using System;
using Confab.Modules.Agendas.Domain.Agendas.Exceptions;
using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Modules.Agendas.Domain.Agendas.Entities
{
    public sealed class RegularAgendaSlot : AgendaSlot
    {
        public int? ParticipantsLimit { get; private set; }
        public AgendaItem AgendaItem { get; private set; }

        public RegularAgendaSlot(EntityId id, DateTime from, DateTime to, AgendaItem agendaItem) : base(id, from, to)
            => AgendaItem = agendaItem;

        private RegularAgendaSlot()
        {
        }
        
        internal RegularAgendaSlot(EntityId id) : base(id)
        {
        }

        internal static RegularAgendaSlot Create(EntityId id, DateTime from, DateTime to, int? participantsLimit)
        {
            var regularAgendaSlot = new RegularAgendaSlot(id);
            regularAgendaSlot.ChangeDateRange(from, to);
            regularAgendaSlot.ChangeParticipantsLimit(participantsLimit);
            
            return regularAgendaSlot;
        }

        internal void ChangeAgendaItem(AgendaItem agendaItem)
            => AgendaItem = agendaItem;

        internal void ChangeParticipantsLimit(int? participantsLimit)
        {
            if (participantsLimit < 0)
            {
                throw new NegativeParticipantsLimitException(Id);
            }

            ParticipantsLimit = participantsLimit;
        }
    }
}