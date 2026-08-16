using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GadeiasBar.Infra.Compartilhado.Orm.Migrations
{
    /// <inheritdoc />
    public partial class Pedidoa_Config : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conta_Mesa_MesaId",
                table: "Conta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mesa",
                table: "Mesa");

            migrationBuilder.RenameTable(
                name: "Mesa",
                newName: "Mesas");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Pedido",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "statusMesa",
                table: "Mesas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mesas",
                table: "Mesas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Conta_Mesas_MesaId",
                table: "Conta",
                column: "MesaId",
                principalTable: "Mesas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conta_Mesas_MesaId",
                table: "Conta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mesas",
                table: "Mesas");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Pedido");

            migrationBuilder.RenameTable(
                name: "Mesas",
                newName: "Mesa");

            migrationBuilder.AlterColumn<int>(
                name: "statusMesa",
                table: "Mesa",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mesa",
                table: "Mesa",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Conta_Mesa_MesaId",
                table: "Conta",
                column: "MesaId",
                principalTable: "Mesa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
