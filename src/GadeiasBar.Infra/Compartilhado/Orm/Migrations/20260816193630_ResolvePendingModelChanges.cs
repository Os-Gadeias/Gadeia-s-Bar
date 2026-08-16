using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GadeiasBar.Infra.Compartilhado.Orm.Migrations
{
    /// <inheritdoc />
    public partial class ResolvePendingModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Garcom",
                newName: "Garcons");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Garcons",
                newName: "Garcom");
        }
    }
}
