using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class atualiza_data_validade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fabricacao",
                table: "Lotes");

            migrationBuilder.DropColumn(
                name: "Validade",
                table: "Lotes");

            migrationBuilder.RenameColumn(
                name: "Quantidade",
                table: "Lotes",
                newName: "UnidadesProdutos");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Validade",
                table: "Produtos",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Validade",
                table: "Produtos");

            migrationBuilder.RenameColumn(
                name: "UnidadesProdutos",
                table: "Lotes",
                newName: "Quantidade");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Fabricacao",
                table: "Lotes",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "Validade",
                table: "Lotes",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }
    }
}
