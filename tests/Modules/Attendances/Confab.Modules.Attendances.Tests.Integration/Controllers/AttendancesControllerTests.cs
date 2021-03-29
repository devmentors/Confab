using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Confab.Modules.Attendances.Application.Clients.Agendas;
using Confab.Modules.Attendances.Application.Clients.Agendas.DTO;
using Confab.Modules.Attendances.Application.DTO;
using Confab.Modules.Attendances.Domain.Entities;
using Confab.Modules.Attendances.Infrastructure.EF;
using Confab.Modules.Attendances.Tests.Integration.Common;
using Confab.Shared.Tests;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Confab.Modules.Attendances.Tests.Integration.Controllers
{
    [Collection("integration")]
    public class AttendancesControllerTests : IClassFixture<TestApplicationFactory>,
        IClassFixture<TestAttendancesDbContext>
    {
        [Fact]
        public async Task get_browse_attendances_without_being_authorized_should_return_unauthorized_status_code()
        {
            var conferenceId = Guid.NewGuid();
            var response = await _client.GetAsync($"{Path}/{conferenceId}");
            response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task get_browse_attendances_given_invalid_participant_should_return_not_found()
        {
            var conferenceId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            Authenticate(userId);
            var response = await _client.GetAsync($"{Path}/{conferenceId}");
            
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task get_browse_attendances_given_valid_conference_and_participant_should_return_all_attendances()
        {
            var from = DateTime.UtcNow;
            var to = from.AddDays(1);
            var conferenceId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var participant = new Participant(Guid.NewGuid(), conferenceId, userId);
            var slot = new Slot(Guid.NewGuid(), participant.Id);
            var attendableEvent = new AttendableEvent(Guid.NewGuid(), conferenceId, from, to, new[] {slot});
            var attendance = new Attendance(Guid.NewGuid(), attendableEvent.Id, slot.Id, participant.Id, from, to);

            await _dbContext.AttendableEvents.AddAsync(attendableEvent);
            await _dbContext.Attendances.AddAsync(attendance);
            await _dbContext.Participants.AddAsync(participant);
            await _dbContext.Slots.AddAsync(slot);
            await _dbContext.SaveChangesAsync();

            _agendasApiClient.GetAgendaAsync(conferenceId).Returns(new List<AgendaTrackDto>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Track 1",
                    ConferenceId = conferenceId,
                    Slots = new[]
                    {
                        new RegularAgendaSlotDto
                        {
                            Id = Guid.NewGuid(),
                            From = from,
                            To = to,
                            AgendaItem = new AgendaItemDto
                            {
                                Id = attendableEvent.Id,
                                Title = "test",
                                Description = "test",
                                Level = 1
                            }
                        }
                    }
                }
            });
            
            Authenticate(userId);
            var response = await _client.GetAsync($"{Path}/{conferenceId}");
            response.IsSuccessStatusCode.ShouldBeTrue();

            var attendances = await response.Content.ReadFromJsonAsync<AttendanceDto[]>();
            attendances.ShouldNotBeEmpty();
            attendances.Length.ShouldBe(1);
        }

        [Fact]
        public async Task post_attend_should_succeed_given_free_slots_and_valid_participant()
        {
            var from = DateTime.UtcNow;
            var to = from.AddDays(1);
            var conferenceId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var participant = new Participant(Guid.NewGuid(), conferenceId, userId);
            var slot = new Slot(Guid.NewGuid());
            var attendableEvent = new AttendableEvent(Guid.NewGuid(), conferenceId, from, to, new[] {slot});

            await _dbContext.AttendableEvents.AddAsync(attendableEvent);
            await _dbContext.Participants.AddAsync(participant);
            await _dbContext.Slots.AddAsync(slot);
            await _dbContext.SaveChangesAsync();
            
            Authenticate(userId);

            var response = await _client.PostAsJsonAsync($"{Path}/events/{attendableEvent.Id}/attend", new {});
            response.IsSuccessStatusCode.ShouldBeTrue();
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        private void Authenticate(Guid userId)
        {
            var jwt = AuthHelper.GenerateJwt(userId.ToString());
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        }
        
        private const string Path = "attendances-module/attendances";
        private readonly IAgendasApiClient _agendasApiClient;
        private HttpClient _client;
        private AttendancesDbContext _dbContext;

        public AttendancesControllerTests(TestApplicationFactory factory, TestAttendancesDbContext dbContext)
        {
            _agendasApiClient = Substitute.For<IAgendasApiClient>();
            _client = factory
                .WithWebHostBuilder(builder => builder.ConfigureServices(services =>
                {
                    services.AddSingleton(_agendasApiClient);
                }))
                .CreateClient();
            _dbContext = dbContext.DbContext;
        }
    }
}