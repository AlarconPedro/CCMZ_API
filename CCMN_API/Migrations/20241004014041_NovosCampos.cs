using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CCMN_API.Migrations
{
    /// <inheritdoc />
    public partial class NovosCampos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PRO_IMAGEM",
                table: "TB_PRODUTOS",
                type: "varchar(max)",
                unicode: false,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PRO_QUANTIDADE_MIN",
                table: "TB_PRODUTOS",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PRO_IMAGEM",
                table: "TB_PRODUTOS");

            migrationBuilder.DropColumn(
                name: "PRO_QUANTIDADE_MIN",
                table: "TB_PRODUTOS");
        }
    }
}
