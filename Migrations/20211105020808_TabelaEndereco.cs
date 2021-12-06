using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace primeira_projeto_aspnetmvc.Migrations
{
    public partial class TabelaEndereco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "endereco",
                schema: "public",
                table: "pessoa");

            migrationBuilder.AddColumn<int>(
                name: "EnderecoId",
                schema: "public",
                table: "pessoa",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "endereco",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    logradouro = table.Column<string>(type: "varchar(256)", nullable: true),
                    numero = table.Column<int>(type: "integer", nullable: false),
                    cep = table.Column<int>(type: "integer", nullable: false),
                    bairro = table.Column<string>(type: "varchar(50)", nullable: true),
                    cidade = table.Column<string>(type: "varchar(30)", nullable: true),
                    estado = table.Column<string>(type: "varchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_endereco", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_pessoa_EnderecoId",
                schema: "public",
                table: "pessoa",
                column: "EnderecoId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pessoa_endereco_EnderecoId",
                schema: "public",
                table: "pessoa");

            migrationBuilder.DropTable(
                name: "endereco",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "IX_pessoa_EnderecoId",
                schema: "public",
                table: "pessoa");

            migrationBuilder.DropColumn(
                name: "EnderecoId",
                schema: "public",
                table: "pessoa");

            migrationBuilder.AddColumn<int>(
                name: "endereco",
                schema: "public",
                table: "pessoa",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
