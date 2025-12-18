using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CA2_Nicolai_de_Groot.Migrations
{
    /// <inheritdoc />
    public partial class AddEventDescriptionColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EventDescription",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventDescription",
                table: "Events");
        }
    }
}
