using Microsoft.EntityFrameworkCore.Migrations;

namespace RiskAssessment.Migrations
{
    public partial class CurrentValueTableCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "currentValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GoldUnitValue = table.Column<int>(type: "int", nullable: false),
                    LandUnitValue = table.Column<int>(type: "int", nullable: false),
                    OtherAssetUnitValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_currentValues", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "currentValues");
        }
    }
}
