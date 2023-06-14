using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewAttachmentTableAndManyToManyRelationShipWithDeal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    File = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttachmentDeal",
                columns: table => new
                {
                    AttachmentsId = table.Column<int>(type: "int", nullable: false),
                    DealsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttachmentDeal", x => new { x.AttachmentsId, x.DealsId });
                    table.ForeignKey(
                        name: "FK_AttachmentDeal_Attachment_AttachmentsId",
                        column: x => x.AttachmentsId,
                        principalTable: "Attachment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttachmentDeal_Deal_DealsId",
                        column: x => x.DealsId,
                        principalTable: "Deal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentDeal_DealsId",
                table: "AttachmentDeal",
                column: "DealsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttachmentDeal");

            migrationBuilder.DropTable(
                name: "Attachment");
        }
    }
}
