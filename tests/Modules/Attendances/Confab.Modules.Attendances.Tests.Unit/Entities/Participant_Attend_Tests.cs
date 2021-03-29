using System;
using System.Collections.Generic;
using System.Linq;
using Confab.Modules.Attendances.Domain.Entities;
using Confab.Modules.Attendances.Domain.Exceptions;
using Shouldly;
using Xunit;

namespace Confab.Modules.Attendances.Tests.Unit.Entities
{
    public class ParticipantTests
    {
        private void Act(Attendance attendance) => _participant.Attend(attendance);

        [Fact]
        public void given_no_colliding_attendances_attend_should_succeed()
        {
            var from = GetDate(9);
            var to = GetDate(10);
            var attendance = new Attendance(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                _participant.Id, from, to);
            Act(attendance);
            _participant.Attendances.Single().ShouldBe(attendance);
        }

        [Fact]
        public void given_existing_attendance_with_same_agenda_item_attend_should_fail()
        {
            var agendaItemId = Guid.NewGuid();
            var from = GetDate(9);
            var to = GetDate(10);
            var attendance1 = new Attendance(Guid.NewGuid(), agendaItemId, Guid.NewGuid(), _participant.Id, from, to);
            var attendance2 = new Attendance(Guid.NewGuid(), agendaItemId, Guid.NewGuid(), _participant.Id, from, to);
            _participant.Attend(attendance1);
            var exception = Record.Exception(() => Act(attendance2));
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<AlreadyParticipatingInEventException>();
        }

        [Theory]
        [MemberData(nameof(GetCollidingDates))]
        public void given_existing_attendance_with_time_collision_attend_should_fail(DateTime from, DateTime to)
        {
            var existingAttendance =
                new Attendance(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), _participant.Id, from, to);
            var nextAttendance = new Attendance(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), _participant.Id,
                GetDate(9, 30), GetDate(11));
            _participant.Attend(existingAttendance);
            var exception = Record.Exception(() => Act(nextAttendance));
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<AlreadyParticipatingSameTimeException>();
        }

        [Theory]
        [MemberData(nameof(GetAvailableDates))]
        public void given_existing_attendance_without_time_collision_attend_should_succeed(DateTime from, DateTime to)
        {
            var existingAttendance = new Attendance(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), _participant.Id, from, to);
            var nextAttendance = new Attendance(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), _participant.Id,
                GetDate(9, 30), GetDate(11));
            _participant.Attend(existingAttendance);
            Act(nextAttendance);
            _participant.Attendances.Last().ShouldBe(nextAttendance);
        }

        private readonly Participant _participant;

        public ParticipantTests()
        {
            _participant = new Participant(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
        }

        public static IEnumerable<object[]> GetCollidingDates()
        {
            yield return new object[] {GetDate(9), GetDate(10)};
            yield return new object[] {GetDate(9), GetDate(11, 30)};
            yield return new object[] {GetDate(10), GetDate(10, 30)};
            yield return new object[] {GetDate(10), GetDate(12)};
        }

        public static IEnumerable<object[]> GetAvailableDates()
        {
            yield return new object[] {GetDate(8), GetDate(9)};
            yield return new object[] {GetDate(8), GetDate(9, 30)};
            yield return new object[] {GetDate(11), GetDate(12)};
            yield return new object[] {GetDate(12), GetDate(13)};
        }

        private static DateTime GetDate(int hour, int minute = 0, int second = 0)
            => new(2020, 1, 1, hour, minute, second);
    }
}