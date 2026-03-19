using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changevaluetype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WishlistEntity_UserId",
                schema: "MySchema",
                table: "WishlistEntity");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistEntity_UserId",
                schema: "MySchema",
                table: "WishlistEntity",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WishlistEntity_UserId",
                schema: "MySchema",
                table: "WishlistEntity");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistEntity_UserId",
                schema: "MySchema",
                table: "WishlistEntity",
                column: "UserId",
                unique: true);
        }
    }
}
