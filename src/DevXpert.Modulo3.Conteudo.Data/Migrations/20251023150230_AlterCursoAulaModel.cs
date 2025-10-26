using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevXpert.Modulo3.ModuloConteudo.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterCursoAulaModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_TITULO_CURSO_AULAS",
                table: "Aulas");

            migrationBuilder.DropColumn(
                name: "CargaHoraria",
                table: "Cursos");

            migrationBuilder.DropColumn(
                name: "PermitirMatricula",
                table: "Cursos");

            migrationBuilder.DropColumn(
                name: "Duracao",
                table: "Aulas");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "Aulas");

            migrationBuilder.RenameColumn(
                name: "Titulo",
                table: "Aulas",
                newName: "Material");

            migrationBuilder.AddColumn<string>(
                name: "Conteudo",
                table: "Aulas",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "UQ_CONTEUDO_CURSO_AULAS",
                table: "Aulas",
                columns: new[] { "Conteudo", "CursoId" },
                unique: true)
                .Annotation("SqlServer:FillFactor", 80);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_CONTEUDO_CURSO_AULAS",
                table: "Aulas");

            migrationBuilder.DropColumn(
                name: "Conteudo",
                table: "Aulas");

            migrationBuilder.RenameColumn(
                name: "Material",
                table: "Aulas",
                newName: "Titulo");

            migrationBuilder.AddColumn<long>(
                name: "CargaHoraria",
                table: "Cursos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "PermitirMatricula",
                table: "Cursos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "Duracao",
                table: "Aulas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Aulas",
                type: "varchar(250)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "UQ_TITULO_CURSO_AULAS",
                table: "Aulas",
                columns: new[] { "Titulo", "CursoId" },
                unique: true)
                .Annotation("SqlServer:FillFactor", 80);
        }
    }
}
