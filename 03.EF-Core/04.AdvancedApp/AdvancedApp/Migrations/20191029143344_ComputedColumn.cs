using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvancedApp.Migrations
{
    public partial class ComputedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GeneratedValue",
                table: "Employees",
                nullable: true,
                computedColumnSql: "SUBSTRING(FirstName, 1, 1) + FamilyName PERSISTED",
                oldClrType: typeof(string),
                oldNullable: true,
                oldDefaultValueSql: "'REFERENCE_'+ CONVERT(varchar, NEXT VALUE FOR ReferenceSequence)");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_GeneratedValue",
                table: "Employees",
                column: "GeneratedValue");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employees_GeneratedValue",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "GeneratedValue",
                table: "Employees",
                nullable: true,
                defaultValueSql: "'REFERENCE_'+ CONVERT(varchar, NEXT VALUE FOR ReferenceSequence)",
                oldClrType: typeof(string),
                oldNullable: true,
                oldComputedColumnSql: "SUBSTRING(FirstName, 1, 1) + FamilyName PERSISTED");
        }
    }
}
