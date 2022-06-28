using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment.Migrations
{
    public partial class addedStatsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatsId",
                table: "Notices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatsId",
                table: "NoticeDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatsId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Stats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stats", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notices_StatsId",
                table: "Notices",
                column: "StatsId");

            migrationBuilder.CreateIndex(
                name: "IX_NoticeDetails_StatsId",
                table: "NoticeDetails",
                column: "StatsId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StatsId",
                table: "AspNetUsers",
                column: "StatsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Stats_StatsId",
                table: "AspNetUsers",
                column: "StatsId",
                principalTable: "Stats",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NoticeDetails_Stats_StatsId",
                table: "NoticeDetails",
                column: "StatsId",
                principalTable: "Stats",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notices_Stats_StatsId",
                table: "Notices",
                column: "StatsId",
                principalTable: "Stats",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Stats_StatsId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_NoticeDetails_Stats_StatsId",
                table: "NoticeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Notices_Stats_StatsId",
                table: "Notices");

            migrationBuilder.DropTable(
                name: "Stats");

            migrationBuilder.DropIndex(
                name: "IX_Notices_StatsId",
                table: "Notices");

            migrationBuilder.DropIndex(
                name: "IX_NoticeDetails_StatsId",
                table: "NoticeDetails");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StatsId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StatsId",
                table: "Notices");

            migrationBuilder.DropColumn(
                name: "StatsId",
                table: "NoticeDetails");

            migrationBuilder.DropColumn(
                name: "StatsId",
                table: "AspNetUsers");
        }
    }
}
