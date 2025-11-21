using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSubscriptionAndParticipation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommunitySubscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CommunityId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunitySubscriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventParticipations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventParticipations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommunitySubscriptions_UserId_CommunityId",
                table: "CommunitySubscriptions",
                columns: new[] { "UserId", "CommunityId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventParticipations_UserId_EventId",
                table: "EventParticipations",
                columns: new[] { "UserId", "EventId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommunitySubscriptions");

            migrationBuilder.DropTable(
                name: "EventParticipations");
        }
    }
}
