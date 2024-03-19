using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetBankingApp.Infrastucture.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddingIsActivaded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActived",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActived",
                table: "Users");
        }
    }
}
