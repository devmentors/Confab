using System.Collections.Generic;
using System.Linq;
using Confab.Modules.Agendas.Domain.Agendas.Exceptions;
using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Modules.Agendas.Domain.Submissions.Exceptions;
using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Modules.Agendas.Domain.Agendas.Entities
{
    public class AgendaItem : AggregateRoot
    {
        public ConferenceId ConferenceId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int Level { get; private set; }
        public IEnumerable<string> Tags { get; private set; }
        public IEnumerable<Speaker> Speakers => _speakers;

        private ICollection<Speaker> _speakers = new List<Speaker>();
        
        public AgendaSlot AgendaSlot { get; private set; }
        
        public AgendaItem(AggregateId id, ConferenceId conferenceId, string title, string description, int level, 
            IEnumerable<string> tags, ICollection<Speaker> speakers, int version = 0) 
        {
            Id = id;
            ConferenceId = conferenceId;
            Title = title;
            Description = description;
            Level = level;
            Tags = tags;
            _speakers = speakers;
            Version = version;
        }

        internal AgendaItem(AggregateId id, ConferenceId conferenceId)
            => (Id, ConferenceId) = (id, conferenceId);

        private AgendaItem()
        {
        }

        public static AgendaItem Create(AggregateId id, ConferenceId conferenceId, string title, string description, 
            int level, IEnumerable<string> tags, ICollection<Speaker> speakers)
        {
            var agendaItem = new AgendaItem(id, conferenceId);
            agendaItem.ChangeTitle(title);
            agendaItem.ChangeDescription(description);
            agendaItem.ChangeLevel(level);
            agendaItem.ChangeTags(tags);
            agendaItem.ChangeSpeakers(speakers);
            agendaItem.Version = 0;
            return agendaItem;
        }

        public void ChangeTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new EmptySubmissionTitleException(Id);
            }

            Title = title;
            IncrementVersion();
        }
        
        public void ChangeDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new EmptySubmissionDescriptionException(Id);
            }

            Description = description;
            IncrementVersion();
        }

        public void ChangeLevel(int level)
        {
            if (IsNotInRange(level))
            {
                throw new InvalidSubmissionLevelException(Id);
            }

            Level = level;

            static bool IsNotInRange(int level) => level < 1 || level > 6;
            IncrementVersion();
        }
        
        public void ChangeTags(IEnumerable<string> tags)
        {
            if (tags is null || !tags.Any())
            {
                throw new EmptyAgendaItemTagsException(Id);
            }

            Tags = tags;
        }
        
        public void ChangeSpeakers(ICollection<Speaker> speakers)
        {
            _speakers = speakers;
            IncrementVersion();
        }
    }
}