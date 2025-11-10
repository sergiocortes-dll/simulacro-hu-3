using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webProductos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "productos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "productos",
                type: "BINARY(8)",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
