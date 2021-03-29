using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Confab.Modules.Attendances.Infrastructure.EF.Migrations
{
    public partial class Attendances_Module_Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "attendances");

            migrationBuilder.CreateTable(
                name: "AttendableEvents",
                schema: "attendances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ConferenceId = table.Column<Guid>(type: "uuid", nullable: true),
                    From = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    To = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendableEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                schema: "attendances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ConferenceId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Slots",
                schema: "attendances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ParticipantId = table.Column<Guid>(type: "uuid", nullable: true),
                    AttendableEventId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Slots_AttendableEvents_AttendableEventId",
                        column: x => x.AttendableEventId,
                        principalSchema: "attendances",
                        principalTable: "AttendableEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attendances",
                schema: "attendances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AttendableEventId = table.Column<Guid>(type: "uuid", nullable: true),
                    SlotId = table.Column<Guid>(type: "uuid", nullable: true),
                    ParticipantId = table.Column<Guid>(type: "uuid", nullable: true),
                    From = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    To = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendances_Participants_ParticipantId",
                        column: x => x.ParticipantId,
                        principalSchema: "attendances",
                        principalTable: "Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_ParticipantId",
                schema: "attendances",
                table: "Attendances",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_UserId_ConferenceId",
                schema: "attendances",
                table: "Participants",
                columns: new[] { "UserId", "ConferenceId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Slots_AttendableEventId",
                schema: "attendances",
                table: "Slots",
                column: "AttendableEventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendances",
                schema: "attendances");

            migrationBuilder.DropTable(
                name: "Slots",
                schema: "attendances");

            migrationBuilder.DropTable(
                name: "Participants",
                schema: "attendances");

            migrationBuilder.DropTable(
                name: "AttendableEvents",
                schema: "attendances");
        }
    }
}
