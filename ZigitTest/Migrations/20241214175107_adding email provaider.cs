using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZigitTest.Migrations
{
    /// <inheritdoc />
    public partial class addingemailprovaider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailProviderName",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmailProviders",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailProviders", x => x.Name);
                });

            migrationBuilder.InsertData(
                table: "EmailProviders",
                column: "Name",
                values: new object[]
                {
                    "gmail",
                    "hotmail",
                    "yahoo"
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmailProviderName",
                table: "Users",
                column: "EmailProviderName");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_EmailProviders_EmailProviderName",
                table: "Users",
                column: "EmailProviderName",
                principalTable: "EmailProviders",
                principalColumn: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_EmailProviders_EmailProviderName",
                table: "Users");

            migrationBuilder.DropTable(
                name: "EmailProviders");

            migrationBuilder.DropIndex(
                name: "IX_Users_EmailProviderName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailProviderName",
                table: "Users");
        }
    }
}
