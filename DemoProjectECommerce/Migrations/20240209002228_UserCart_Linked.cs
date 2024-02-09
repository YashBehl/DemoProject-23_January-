using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoProjectECommerce.Migrations
{
    /// <inheritdoc />
    public partial class UserCart_Linked : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userCartId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userCartId",
                table: "AspNetUsers");
        }
    }
}
