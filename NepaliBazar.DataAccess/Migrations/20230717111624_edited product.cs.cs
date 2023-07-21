using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NepaliBazar.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class editedproductcs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "actualPrice",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "actualPrice",
                table: "Products");
        }
    }
}
