using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RulesSimulator.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Rules",
                newName: "symbol");

            migrationBuilder.AddColumn<decimal>(
                name: "MaxPrice",
                table: "Rules",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MaxQty",
                table: "Rules",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MinPrice",
                table: "Rules",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MinQty",
                table: "Rules",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ruleTypeID",
                table: "Rules",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxPrice",
                table: "Rules");

            migrationBuilder.DropColumn(
                name: "MaxQty",
                table: "Rules");

            migrationBuilder.DropColumn(
                name: "MinPrice",
                table: "Rules");

            migrationBuilder.DropColumn(
                name: "MinQty",
                table: "Rules");

            migrationBuilder.DropColumn(
                name: "ruleTypeID",
                table: "Rules");

            migrationBuilder.RenameColumn(
                name: "symbol",
                table: "Rules",
                newName: "Name");
        }
    }
}
