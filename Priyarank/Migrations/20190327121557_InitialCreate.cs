using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Priyarank.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValue: Guid.NewGuid()),
                    Name = table.Column<string>(nullable: false),
                    Elo = table.Column<int>(nullable: false, defaultValue: 1200),
                    Wins = table.Column<int>(nullable: false),
                    Losses = table.Column<int>(nullable: false),
                    Draws = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValue: Guid.NewGuid()),
                    Team1Id = table.Column<Guid>(nullable: false),
                    Team2Id = table.Column<Guid>(nullable: false),
                    winnerId = table.Column<Guid>(nullable: true),
                    loserId = table.Column<Guid>(nullable: true),
                    _played = table.Column<bool>(nullable: false),
                    created = table.Column<DateTime>(nullable: false, defaultValue: DateTime.UtcNow),
                    _playedOn = table.Column<DateTime>(nullable: false),
                    draw = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Match_Team_Team1Id",
                        column: x => x.Team1Id,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_Team_Team2Id",
                        column: x => x.Team2Id,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_Team_loserId",
                        column: x => x.loserId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_Team_winnerId",
                        column: x => x.winnerId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Match_Team1Id",
                table: "Match",
                column: "Team1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Match_Team2Id",
                table: "Match",
                column: "Team2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Match_loserId",
                table: "Match",
                column: "loserId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_winnerId",
                table: "Match",
                column: "winnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Match");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Team");
        }
    }
}
