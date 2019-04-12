using Microsoft.EntityFrameworkCore.Migrations;

namespace MinhasFinancas.Migrations
{
    public partial class AdicionaNomeItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ItemNome",
                table: "RelatorioDespesas",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemNome",
                table: "RelatorioDespesas");
        }
    }
}
