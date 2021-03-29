using System;
using Confab.Modules.Agendas.Domain.CallForPapers.Exceptions;
using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Modules.Agendas.Domain.CallForPapers.Entities
{
    public sealed class CallForPapers : AggregateRoot
    {
        public ConferenceId ConferenceId { get; private set; }
        public DateTime From { get; private set; }
        public DateTime To { get; private set; }
        public bool IsOpened { get; private set; }

        public CallForPapers(AggregateId id, ConferenceId conferenceId, DateTime @from, DateTime to, bool isOpened, 
            int version = 0)
        {
            Id = id;
            ConferenceId = conferenceId;
            From = @from;
            To = to;
            IsOpened = isOpened;
            Version = version;
        }

        public CallForPapers()
        {
        }

        internal CallForPapers(AggregateId id)
            => Id = id;

        public static CallForPapers Create(AggregateId id, Guid conferenceId, DateTime from, DateTime to)
        {
            var callForPapers = new CallForPapers(id);
            callForPapers.ConferenceId = conferenceId;
            callForPapers.ChangeDateRange(from, to);
            callForPapers.IsOpened = false;
            callForPapers.ClearEvents();
            callForPapers.Version = 0;

            return callForPapers;
        }

        public void ChangeDateRange(DateTime from, DateTime to)
        {
            if (from.Date > to.Date)
            {
                throw new InvalidCallForPapersDatesException(from, to);
            }

            From = from;
            To = to;
            IncrementVersion();
        }

        public void Open()
        {
            IsOpened = true;
            IncrementVersion();
        }
        
        public void Close()
        {
            IsOpened = false;
            IncrementVersion();
        }
    }
}