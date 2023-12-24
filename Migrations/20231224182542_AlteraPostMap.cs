using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blogg.Migrations
{
    /// <inheritdoc />
    public partial class AlteraPostMap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_TagId",
                table: "PostTag");

            migrationBuilder.AddForeignKey(
                name: "FK_PostRole_TagId",
                table: "PostTag",
                column: "TagId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostRole_TagId",
                table: "PostTag");

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_TagId",
                table: "PostTag",
                column: "TagId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
