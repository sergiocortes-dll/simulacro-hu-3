using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webProductos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial3 : Migration
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
                oldType: "longblob");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "productos",
                type: "longblob",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "BINARY(8)",
                oldRowVersion: true);
        }
    }
}
