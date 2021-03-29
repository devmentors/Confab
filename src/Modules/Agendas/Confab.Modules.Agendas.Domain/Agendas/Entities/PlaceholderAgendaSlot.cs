using System;
using Confab.Modules.Agendas.Domain.Agendas.Exceptions;
using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Modules.Agendas.Domain.Agendas.Entities
{
    public sealed class PlaceholderAgendaSlot : AgendaSlot
    {
        public string Placeholder { get; private set; }

        public PlaceholderAgendaSlot(EntityId id, DateTime from, DateTime to, string placeholder) : base(id, from, to)
            => Placeholder = placeholder;

        private PlaceholderAgendaSlot()
        {
        }
        
        internal PlaceholderAgendaSlot(EntityId id) : base(id)
        {
        }

        internal static PlaceholderAgendaSlot Create(EntityId id, DateTime from, DateTime to)
        {
            var placeholderAgendaSlot = new PlaceholderAgendaSlot(id);
            placeholderAgendaSlot.ChangeDateRange(from, to);

            return placeholderAgendaSlot;
        }

        public void ChangePlaceholder(string placeholder)
        {
            if (string.IsNullOrEmpty(placeholder))
            {
                throw new EmptyAgendaSlotPlaceholderException();
            }

            Placeholder = placeholder;
        }
    }
}