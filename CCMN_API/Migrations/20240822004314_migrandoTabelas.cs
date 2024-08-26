using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CCMN_API.Migrations
{
    /// <inheritdoc />
    public partial class migrandoTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_CATEGORIAS",
                columns: table => new
                {
                    CAT_CODIGO = table.Column<int>(type: "int", nullable: false),
                    CAT_NOME = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CATEGORIAS", x => x.CAT_CODIGO);
                });

            migrationBuilder.CreateTable(
                name: "TB_PRODUTOS",
                columns: table => new
                {
                    PRO_CODIGO = table.Column<int>(type: "int", nullable: false),
                    PRO_NOME = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    PRO_COD_BARRAS = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    PRO_VALOR = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    PRO_MEDIDA = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    PRO_UNI_MEDIDA = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: true),
                    PRO_QUANTIDADE = table.Column<int>(type: "int", nullable: true),
                    CAT_CODIGO = table.Column<int>(type: "int", nullable: true),
                    PRO_DESCRICAO = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PRODUTOS", x => x.PRO_CODIGO);
                    table.ForeignKey(
                        name: "CATEGORIA",
                        column: x => x.CAT_CODIGO,
                        principalTable: "TB_CATEGORIAS",
                        principalColumn: "CAT_CODIGO");
                });

            migrationBuilder.CreateTable(
                name: "TB_MOVIMENTO_PRODUTOS",
                columns: table => new
                {
                    MOV_CODIGO = table.Column<int>(type: "int", nullable: false),
                    PRO_CODIGO = table.Column<int>(type: "int", nullable: true),
                    MOV_QUANTIDADE = table.Column<int>(type: "int", nullable: true),
                    MOV_DATA = table.Column<DateTime>(type: "datetime", nullable: true),
                    MOV_TIPO = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_MOVIMENTO_ESTOQUE", x => x.MOV_CODIGO);
                    table.ForeignKey(
                        name: "MOVIMENTO_PRODUTO",
                        column: x => x.PRO_CODIGO,
                        principalTable: "TB_PRODUTOS",
                        principalColumn: "PRO_CODIGO");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_MOVIMENTO_PRODUTOS_PRO_CODIGO",
                table: "TB_MOVIMENTO_PRODUTOS",
                column: "PRO_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PRODUTOS_CAT_CODIGO",
                table: "TB_PRODUTOS",
                column: "CAT_CODIGO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_MOVIMENTO_PRODUTOS");

            migrationBuilder.DropTable(
                name: "TB_PRODUTOS");

            migrationBuilder.DropTable(
                name: "TB_CATEGORIAS");

        }
    }
}
