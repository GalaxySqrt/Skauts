using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skauts.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "event_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__event_ty__3213E83F78F538D9", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    image_path = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "('GETDATE()')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__organiza__3213E83F602687DF", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "prize_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__prize_ty__3213E83FEB4156D5", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    acronym = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    name = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__roles__3213E83F0010D88D", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    password_hash = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "('GETDATE()')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__users__3213E83F569A8781", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "championships",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    org_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__champion__3213E83FBCF333E6", x => x.id);
                    table.ForeignKey(
                        name: "FK__champions__org_i__5DCAEF64",
                        column: x => x.org_id,
                        principalTable: "organizations",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "teams",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    org_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "('GETDATE()')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__teams__3213E83F95B63308", x => x.id);
                    table.ForeignKey(
                        name: "FK__teams__org_id__5BE2A6F2",
                        column: x => x.org_id,
                        principalTable: "organizations",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "players",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    org_id = table.Column<int>(type: "int", nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false),
                    skill = table.Column<int>(type: "int", nullable: true, defaultValue: 6),
                    physique = table.Column<int>(type: "int", nullable: true, defaultValue: 6),
                    phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    image_path = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "('GETDATE()')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__players__3213E83FE96945F2", x => x.id);
                    table.ForeignKey(
                        name: "FK__players__org_id__5AEE82B9",
                        column: x => x.org_id,
                        principalTable: "organizations",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__players__role_id__5EBF139D",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "users_organizations",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false),
                    org_id = table.Column<int>(type: "int", nullable: false),
                    admin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__users_or__86D4EF0E96E8092D", x => new { x.user_id, x.org_id });
                    table.ForeignKey(
                        name: "FK__users_org__org_i__59FA5E80",
                        column: x => x.org_id,
                        principalTable: "organizations",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__users_org__user___59063A47",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "matches",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    org_id = table.Column<int>(type: "int", nullable: false),
                    team_a_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    team_b_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    championship_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__matches__3213E83F5537E76A", x => x.id);
                    table.ForeignKey(
                        name: "FK__matches__champio__6383C8BA",
                        column: x => x.championship_id,
                        principalTable: "championships",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__matches__org_id__5CD6CB2B",
                        column: x => x.org_id,
                        principalTable: "organizations",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__matches__team_a___619B8048",
                        column: x => x.team_a_id,
                        principalTable: "teams",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__matches__team_b___628FA481",
                        column: x => x.team_b_id,
                        principalTable: "teams",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "players_prizes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    player_id = table.Column<int>(type: "int", nullable: false),
                    prize_type_id = table.Column<int>(type: "int", nullable: false),
                    receive_date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__players___3213E83F63CEB117", x => x.id);
                    table.ForeignKey(
                        name: "FK__players_p__playe__6754599E",
                        column: x => x.player_id,
                        principalTable: "players",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__players_p__prize__68487DD7",
                        column: x => x.prize_type_id,
                        principalTable: "prize_types",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "team_players",
                columns: table => new
                {
                    team_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    player_id = table.Column<int>(type: "int", nullable: false),
                    join_date = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "('GETDATE()')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__team_pla__2C604C9C40110C4F", x => new { x.team_id, x.player_id });
                    table.ForeignKey(
                        name: "FK__team_play__playe__5FB337D6",
                        column: x => x.player_id,
                        principalTable: "players",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__team_play__team___60A75C0F",
                        column: x => x.team_id,
                        principalTable: "teams",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    match_id = table.Column<int>(type: "int", nullable: false),
                    player_id = table.Column<int>(type: "int", nullable: false),
                    event_type_id = table.Column<int>(type: "int", nullable: false),
                    event_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "('GETDATE()')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__events__3213E83F2B14DB10", x => x.id);
                    table.ForeignKey(
                        name: "FK__events__event_ty__66603565",
                        column: x => x.event_type_id,
                        principalTable: "event_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__events__match_id__6477ECF3",
                        column: x => x.match_id,
                        principalTable: "matches",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__events__player_i__656C112C",
                        column: x => x.player_id,
                        principalTable: "players",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_championships_org_id",
                table: "championships",
                column: "org_id");

            migrationBuilder.CreateIndex(
                name: "IX_events_event_type_id",
                table: "events",
                column: "event_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_events_match_id",
                table: "events",
                column: "match_id");

            migrationBuilder.CreateIndex(
                name: "IX_events_player_id",
                table: "events",
                column: "player_id");

            migrationBuilder.CreateIndex(
                name: "IX_matches_championship_id",
                table: "matches",
                column: "championship_id");

            migrationBuilder.CreateIndex(
                name: "IX_matches_org_id",
                table: "matches",
                column: "org_id");

            migrationBuilder.CreateIndex(
                name: "IX_matches_team_a_id",
                table: "matches",
                column: "team_a_id");

            migrationBuilder.CreateIndex(
                name: "IX_matches_team_b_id",
                table: "matches",
                column: "team_b_id");

            migrationBuilder.CreateIndex(
                name: "IX_players_org_id",
                table: "players",
                column: "org_id");

            migrationBuilder.CreateIndex(
                name: "IX_players_role_id",
                table: "players",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_players_prizes_player_id",
                table: "players_prizes",
                column: "player_id");

            migrationBuilder.CreateIndex(
                name: "IX_players_prizes_prize_type_id",
                table: "players_prizes",
                column: "prize_type_id");

            migrationBuilder.CreateIndex(
                name: "UQ__roles__8172A53149FC0CF8",
                table: "roles",
                column: "acronym",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_team_players_player_id",
                table: "team_players",
                column: "player_id");

            migrationBuilder.CreateIndex(
                name: "IX_teams_org_id",
                table: "teams",
                column: "org_id");

            migrationBuilder.CreateIndex(
                name: "UQ__users__AB6E61642E4A9832",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_organizations_org_id",
                table: "users_organizations",
                column: "org_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "players_prizes");

            migrationBuilder.DropTable(
                name: "team_players");

            migrationBuilder.DropTable(
                name: "users_organizations");

            migrationBuilder.DropTable(
                name: "event_types");

            migrationBuilder.DropTable(
                name: "matches");

            migrationBuilder.DropTable(
                name: "prize_types");

            migrationBuilder.DropTable(
                name: "players");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "championships");

            migrationBuilder.DropTable(
                name: "teams");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "organizations");
        }
    }
}
