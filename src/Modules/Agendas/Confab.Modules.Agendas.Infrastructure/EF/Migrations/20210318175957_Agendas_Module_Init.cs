using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Confab.Modules.Agendas.Infrastructure.EF.Migrations
{
    public partial class Agendas_Module_Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "agendas");

            migrationBuilder.CreateTable(
                name: "AgendaTracks",
                schema: "agendas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ConferenceId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendaTracks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CallForPapers",
                schema: "agendas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ConferenceId = table.Column<Guid>(type: "uuid", nullable: true),
                    From = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    To = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsOpened = table.Column<bool>(type: "boolean", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallForPapers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Speakers",
                schema: "agendas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speakers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Submissions",
                schema: "agendas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ConferenceId = table.Column<Guid>(type: "uuid", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true),
                    Tags = table.Column<string>(type: "text", nullable: true),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpeakerSubmission",
                schema: "agendas",
                columns: table => new
                {
                    SpeakersId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubmissionsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeakerSubmission", x => new { x.SpeakersId, x.SubmissionsId });
                    table.ForeignKey(
                        name: "FK_SpeakerSubmission_Speakers_SpeakersId",
                        column: x => x.SpeakersId,
                        principalSchema: "agendas",
                        principalTable: "Speakers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpeakerSubmission_Submissions_SubmissionsId",
                        column: x => x.SubmissionsId,
                        principalSchema: "agendas",
                        principalTable: "Submissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgendaItemSpeaker",
                schema: "agendas",
                columns: table => new
                {
                    AgendaItemsId = table.Column<Guid>(type: "uuid", nullable: false),
                    SpeakersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendaItemSpeaker", x => new { x.AgendaItemsId, x.SpeakersId });
                    table.ForeignKey(
                        name: "FK_AgendaItemSpeaker_Speakers_SpeakersId",
                        column: x => x.SpeakersId,
                        principalSchema: "agendas",
                        principalTable: "Speakers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgendaSlots",
                schema: "agendas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    From = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    To = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TrackId = table.Column<Guid>(type: "uuid", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Placeholder = table.Column<string>(type: "text", nullable: true),
                    ParticipantsLimit = table.Column<int>(type: "integer", nullable: true),
                    AgendaItemId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendaSlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgendaSlots_AgendaTracks_TrackId",
                        column: x => x.TrackId,
                        principalSchema: "agendas",
                        principalTable: "AgendaTracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AgendaItems",
                schema: "agendas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ConferenceId = table.Column<Guid>(type: "uuid", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Tags = table.Column<string>(type: "text", nullable: true),
                    AgendaSlotId = table.Column<Guid>(type: "uuid", nullable: true),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendaItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgendaItems_AgendaSlots_AgendaSlotId",
                        column: x => x.AgendaSlotId,
                        principalSchema: "agendas",
                        principalTable: "AgendaSlots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgendaItems_AgendaSlotId",
                schema: "agendas",
                table: "AgendaItems",
                column: "AgendaSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_AgendaItemSpeaker_SpeakersId",
                schema: "agendas",
                table: "AgendaItemSpeaker",
                column: "SpeakersId");

            migrationBuilder.CreateIndex(
                name: "IX_AgendaSlots_AgendaItemId",
                schema: "agendas",
                table: "AgendaSlots",
                column: "AgendaItemId");

            migrationBuilder.CreateIndex(
                name: "IX_AgendaSlots_TrackId",
                schema: "agendas",
                table: "AgendaSlots",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_SpeakerSubmission_SubmissionsId",
                schema: "agendas",
                table: "SpeakerSubmission",
                column: "SubmissionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AgendaItemSpeaker_AgendaItems_AgendaItemsId",
                schema: "agendas",
                table: "AgendaItemSpeaker",
                column: "AgendaItemsId",
                principalSchema: "agendas",
                principalTable: "AgendaItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AgendaSlots_AgendaItems_AgendaItemId",
                schema: "agendas",
                table: "AgendaSlots",
                column: "AgendaItemId",
                principalSchema: "agendas",
                principalTable: "AgendaItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgendaItems_AgendaSlots_AgendaSlotId",
                schema: "agendas",
                table: "AgendaItems");

            migrationBuilder.DropTable(
                name: "AgendaItemSpeaker",
                schema: "agendas");

            migrationBuilder.DropTable(
                name: "CallForPapers",
                schema: "agendas");

            migrationBuilder.DropTable(
                name: "SpeakerSubmission",
                schema: "agendas");

            migrationBuilder.DropTable(
                name: "Speakers",
                schema: "agendas");

            migrationBuilder.DropTable(
                name: "Submissions",
                schema: "agendas");

            migrationBuilder.DropTable(
                name: "AgendaSlots",
                schema: "agendas");

            migrationBuilder.DropTable(
                name: "AgendaItems",
                schema: "agendas");

            migrationBuilder.DropTable(
                name: "AgendaTracks",
                schema: "agendas");
        }
    }
}
