using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class IdentityAdded4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Deal_DlPriorityId",
                table: "Deal");

            migrationBuilder.CreateIndex(
                name: "IX_Deal_DlPriorityId",
                table: "Deal",
                column: "DlPriorityId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Deal_DlPriorityId",
                table: "Deal");

            migrationBuilder.CreateIndex(
                name: "IX_Deal_DlPriorityId",
                table: "Deal",
                column: "DlPriorityId");
        }
    }
}
