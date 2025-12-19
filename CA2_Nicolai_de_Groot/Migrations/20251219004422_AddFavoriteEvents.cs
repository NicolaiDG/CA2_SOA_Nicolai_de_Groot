using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CA2_Nicolai_de_Groot.Migrations
{
    /// <inheritdoc />
    public partial class AddFavoriteEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FavoriteEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Auth0UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteEvents_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteEvents_Auth0UserId_EventId",
                table: "FavoriteEvents",
                columns: new[] { "Auth0UserId", "EventId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteEvents_EventId",
                table: "FavoriteEvents",
                column: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoriteEvents");
        }
    }
}
