using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skauts.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlFile = Path.Combine(AppContext.BaseDirectory, "Data/Seed/Seed.sql");

            if (!File.Exists(sqlFile))
            {
                throw new FileNotFoundException("Script de Seed não encontrado", sqlFile);
            }

            migrationBuilder.Sql(File.ReadAllText(sqlFile));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Se dermos 'rollback', podemos apagar os dados.
            // Isso é opcional, mas uma boa prática.
            migrationBuilder.Sql(@"
                DELETE FROM events;
                DELETE FROM players_prizes;
                DELETE FROM team_players;
                DELETE FROM matches;
                DELETE FROM championships;
                DELETE FROM teams;
                DELETE FROM players;
                DELETE FROM users_organizations;
                DELETE FROM organizations;
                DELETE FROM users;
                DELETE FROM roles;
                DELETE FROM event_types;
                DELETE FROM prize_types;
            ");
        }
    }
}
