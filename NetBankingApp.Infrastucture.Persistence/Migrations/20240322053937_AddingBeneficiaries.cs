using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetBankingApp.Infrastucture.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddingBeneficiaries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Beneficiaries",
                columns: table => new
                {
                    IdUser = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdBeneficiary = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BeneficiaryAccountGuid = table.Column<int>(type: "int", nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beneficiaries", x => new { x.IdUser, x.IdBeneficiary });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Beneficiaries");
        }
    }
}
