using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CCMN_API.Migrations
{
    /// <inheritdoc />
    public partial class correcao : Migration
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
                name: "TB_FORNECEDORES",
                columns: table => new
                {
                    FOR_CODIGO = table.Column<int>(type: "int", nullable: false),
                    FOR_NOME = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_FORNECEDORES", x => x.FOR_CODIGO);
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
                name: "TB_FORMULARIOS",
                columns: table => new
                {
                    FOR_CODIGO = table.Column<int>(type: "int", nullable: false),
                    FOR_NOME = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    FOR_ENDERECO = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    COM_CODIGO = table.Column<int>(type: "int", nullable: true),
                    EVE_CODIGO = table.Column<int>(type: "int", nullable: true),
                    FOR_DATACRIACAO = table.Column<DateTime>(type: "datetime", nullable: true),
                    FOR_STATUS = table.Column<bool>(type: "bit", nullable: true),
                    FOR_TIPO = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_FORMULARIOS", x => x.FOR_CODIGO);
                    table.ForeignKey(
                        name: "ComunidadeFormulario",
                        column: x => x.COM_CODIGO,
                        principalTable: "TB_COMUNIDADE",
                        principalColumn: "COM_CODIGO");
                    table.ForeignKey(
                        name: "EventoFomulario",
                        column: x => x.EVE_CODIGO,
                        principalTable: "TB_EVENTOS",
                        principalColumn: "EVE_CODIGO");
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
                name: "IX_TB_CASAIS_CAS_ESPOSA",
                table: "TB_CASAIS",
                column: "CAS_ESPOSA");

            migrationBuilder.CreateIndex(
                name: "IX_TB_CASAIS_CAS_ESPOSO",
                table: "TB_CASAIS",
                column: "CAS_ESPOSO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_DESPESA_COMUNIDADE_EVENTO_COM_CODIGO",
                table: "TB_DESPESA_COMUNIDADE_EVENTO",
                column: "COM_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_DESPESA_COMUNIDADE_EVENTO_EVE_CODIGO",
                table: "TB_DESPESA_COMUNIDADE_EVENTO",
                column: "EVE_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_DESPESA_EVENTO_EVE_CODIGO",
                table: "TB_DESPESA_EVENTO",
                column: "EVE_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_EVENTO_PESSOAS_EVE_CODIGO",
                table: "TB_EVENTO_PESSOAS",
                column: "EVE_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_EVENTO_PESSOAS_PES_CODIGO",
                table: "TB_EVENTO_PESSOAS",
                column: "PES_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_EVENTO_QUARTOS_EVE_CODIGO",
                table: "TB_EVENTO_QUARTOS",
                column: "EVE_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_EVENTO_QUARTOS_QUA_CODIGO",
                table: "TB_EVENTO_QUARTOS",
                column: "QUA_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_FORMULARIOS_COM_CODIGO",
                table: "TB_FORMULARIOS",
                column: "COM_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_FORMULARIOS_EVE_CODIGO",
                table: "TB_FORMULARIOS",
                column: "EVE_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_MOVIMENTO_PRODUTOS_PRO_CODIGO",
                table: "TB_MOVIMENTO_PRODUTOS",
                column: "PRO_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PARTICIPANTES_CUPONS_CUP_CODIGO",
                table: "TB_PARTICIPANTES_CUPONS",
                column: "CUP_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PARTICIPANTES_CUPONS_PAR_CODIGO",
                table: "TB_PARTICIPANTES_CUPONS",
                column: "PAR_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PESSOAS_COM_CODIGO",
                table: "TB_PESSOAS",
                column: "COM_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PRODUTOS_CAT_CODIGO",
                table: "TB_PRODUTOS",
                column: "CAT_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PROMOCOES_CUPONS_PAR_CODIGO",
                table: "TB_PROMOCOES_CUPONS",
                column: "PAR_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PROMOCOES_CUPONS_PRO_CODIGO",
                table: "TB_PROMOCOES_CUPONS",
                column: "PRO_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PROMOCOES_PARTICIPANTES_PRO_CODIGO",
                table: "TB_PROMOCOES_PARTICIPANTES",
                column: "PRO_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PROMOCOES_PREMIOS_PRO_CODIGO",
                table: "TB_PROMOCOES_PREMIOS",
                column: "PRO_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PROMOCOES_SORTEIO_CUP_CODIGO",
                table: "TB_PROMOCOES_SORTEIO",
                column: "CUP_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PROMOCOES_SORTEIO_PAR_CODIGO",
                table: "TB_PROMOCOES_SORTEIO",
                column: "PAR_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PROMOCOES_SORTEIO_PRE_CODIGO",
                table: "TB_PROMOCOES_SORTEIO",
                column: "PRE_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PROMOCOES_SORTEIO_PRO_CODIGO",
                table: "TB_PROMOCOES_SORTEIO",
                column: "PRO_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_QUARTO_PESSOAS_PES_CODIGO",
                table: "TB_QUARTO_PESSOAS",
                column: "PES_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_QUARTO_PESSOAS_QUA_CODIGO",
                table: "TB_QUARTO_PESSOAS",
                column: "QUA_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_QUARTOS_BLO_CODIGO",
                table: "TB_QUARTOS",
                column: "BLO_CODIGO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_CASAIS");

            migrationBuilder.DropTable(
                name: "TB_DESPESA_COMUNIDADE_EVENTO");

            migrationBuilder.DropTable(
                name: "TB_DESPESA_EVENTO");

            migrationBuilder.DropTable(
                name: "TB_EVENTO_PESSOAS");

            migrationBuilder.DropTable(
                name: "TB_EVENTO_QUARTOS");

            migrationBuilder.DropTable(
                name: "TB_FORMULARIOS");

            migrationBuilder.DropTable(
                name: "TB_FORNECEDORES");

            migrationBuilder.DropTable(
                name: "TB_MOVIMENTO_PRODUTOS");

            migrationBuilder.DropTable(
                name: "TB_PARTICIPANTES_CUPONS");

            migrationBuilder.DropTable(
                name: "TB_PROMOCOES_SORTEIO");

            migrationBuilder.DropTable(
                name: "TB_QUARTO_PESSOAS");

            migrationBuilder.DropTable(
                name: "TB_USUARIOS");

            migrationBuilder.DropTable(
                name: "TB_EVENTOS");

            migrationBuilder.DropTable(
                name: "TB_PRODUTOS");

            migrationBuilder.DropTable(
                name: "TB_PROMOCOES_CUPONS");

            migrationBuilder.DropTable(
                name: "TB_PROMOCOES_PREMIOS");

            migrationBuilder.DropTable(
                name: "TB_PESSOAS");

            migrationBuilder.DropTable(
                name: "TB_QUARTOS");

            migrationBuilder.DropTable(
                name: "TB_CATEGORIAS");

            migrationBuilder.DropTable(
                name: "TB_PROMOCOES_PARTICIPANTES");

            migrationBuilder.DropTable(
                name: "TB_COMUNIDADE");

            migrationBuilder.DropTable(
                name: "TB_BLOCOS");

            migrationBuilder.DropTable(
                name: "TB_PROMOCOES");
        }
    }
}
