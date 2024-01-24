using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace iscaslune.Api.Migrations
{
    /// <inheritdoc />
    public partial class TabelaPreco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrecosProduto");

            migrationBuilder.DropTable(
                name: "PrecosProdutoPeso");

            migrationBuilder.CreateTable(
                name: "TabelaDePreco",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Descricao = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    AtivaEcommerce = table.Column<bool>(type: "boolean", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    Numero = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaDePreco", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItensTabelaDePreco",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ValorUnitario = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    TabelaDePrecoId = table.Column<Guid>(type: "uuid", nullable: false),
                    TamanhoId = table.Column<Guid>(type: "uuid", nullable: true),
                    PesoId = table.Column<Guid>(type: "uuid", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    Numero = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensTabelaDePreco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensTabelaDePreco_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItensTabelaDePreco_TabelaDePreco_TabelaDePrecoId",
                        column: x => x.TabelaDePrecoId,
                        principalTable: "TabelaDePreco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItensTabelaDePreco_ProdutoId",
                table: "ItensTabelaDePreco",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensTabelaDePreco_TabelaDePrecoId",
                table: "ItensTabelaDePreco",
                column: "TabelaDePrecoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItensTabelaDePreco");

            migrationBuilder.DropTable(
                name: "TabelaDePreco");

            migrationBuilder.CreateTable(
                name: "PrecosProduto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TamanhoId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    DataCriacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    Numero = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Preco = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    PrecoCusto = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: true),
                    PrecoPromocional = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrecosProduto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrecosProduto_Tamanhos_TamanhoId",
                        column: x => x.TamanhoId,
                        principalTable: "Tamanhos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrecosProdutoPeso",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PesoId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    DataCriacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    Numero = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Preco = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    PrecoCusto = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: true),
                    PrecoPromocional = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrecosProdutoPeso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrecosProdutoPeso_Pesos_PesoId",
                        column: x => x.PesoId,
                        principalTable: "Pesos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrecosProduto_TamanhoId",
                table: "PrecosProduto",
                column: "TamanhoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrecosProdutoPeso_PesoId",
                table: "PrecosProdutoPeso",
                column: "PesoId",
                unique: true);
        }
    }
}
