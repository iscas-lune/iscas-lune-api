using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iscaslune.Api.Migrations
{
    /// <inheritdoc />
    public partial class PrecoProdutoTamanho : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrecosProduto_Produtos_ProdutoId",
                table: "PrecosProduto");

            migrationBuilder.RenameColumn(
                name: "ProdutoId",
                table: "PrecosProduto",
                newName: "TamanhoId");

            migrationBuilder.RenameIndex(
                name: "IX_PrecosProduto_ProdutoId",
                table: "PrecosProduto",
                newName: "IX_PrecosProduto_TamanhoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrecosProduto_Tamanhos_TamanhoId",
                table: "PrecosProduto",
                column: "TamanhoId",
                principalTable: "Tamanhos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrecosProduto_Tamanhos_TamanhoId",
                table: "PrecosProduto");

            migrationBuilder.RenameColumn(
                name: "TamanhoId",
                table: "PrecosProduto",
                newName: "ProdutoId");

            migrationBuilder.RenameIndex(
                name: "IX_PrecosProduto_TamanhoId",
                table: "PrecosProduto",
                newName: "IX_PrecosProduto_ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrecosProduto_Produtos_ProdutoId",
                table: "PrecosProduto",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
