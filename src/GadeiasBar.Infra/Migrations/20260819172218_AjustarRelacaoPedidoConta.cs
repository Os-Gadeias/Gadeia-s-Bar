using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GadeiasBar.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AjustarRelacaoPedidoConta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_Conta_ContaId",
                table: "Pedido");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_Conta_ContaId",
                table: "Pedido",
                column: "ContaId",
                principalTable: "Conta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_Conta_ContaId",
                table: "Pedido");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_Conta_ContaId",
                table: "Pedido",
                column: "ContaId",
                principalTable: "Conta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
