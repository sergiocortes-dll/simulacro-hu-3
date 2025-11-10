using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webProductos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "productos",
                type: "BINARY(8)",
                rowVersion: true,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "BINARY(8)",
                oldRowVersion: true,
                oldDefaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "productos",
                type: "BINARY(8)",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 },
                oldClrType: typeof(byte[]),
                oldType: "BINARY(8)",
                oldRowVersion: true);
        }
    }
}
