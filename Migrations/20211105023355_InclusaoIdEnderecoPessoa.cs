using Microsoft.EntityFrameworkCore.Migrations;

namespace primeira_projeto_aspnetmvc.Migrations
{
    public partial class InclusaoIdEnderecoPessoa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pessoa_endereco_EnderecoId",
                schema: "public",
                table: "pessoa");

            migrationBuilder.AlterColumn<int>(
                name: "EnderecoId",
                schema: "public",
                table: "pessoa",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_pessoa_endereco_EnderecoId",
                schema: "public",
                table: "pessoa",
                column: "EnderecoId",
                principalSchema: "public",
                principalTable: "endereco",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pessoa_endereco_EnderecoId",
                schema: "public",
                table: "pessoa");

            migrationBuilder.AlterColumn<int>(
                name: "EnderecoId",
                schema: "public",
                table: "pessoa",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_pessoa_endereco_EnderecoId",
                schema: "public",
                table: "pessoa",
                column: "EnderecoId",
                principalSchema: "public",
                principalTable: "endereco",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
