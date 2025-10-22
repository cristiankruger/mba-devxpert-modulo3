using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevXpert.Modulo3.ModuloConteudo.Data.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    PermitirMatricula = table.Column<bool>(type: "bit", nullable: false),
                    CargaHoraria = table.Column<TimeSpan>(type: "bigint", nullable: false),
                    Ementa = table.Column<string>(type: "varchar(1000)", nullable: true),
                    Instrutor = table.Column<string>(type: "varchar(100)", nullable: true),
                    PublicoAlvo = table.Column<string>(type: "varchar(250)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Aulas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CursoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Link = table.Column<string>(type: "varchar(250)", nullable: false),
                    Titulo = table.Column<string>(type: "varchar(100)", nullable: false),
                    Duracao = table.Column<TimeSpan>(type: "bigint", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aulas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aulas_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aulas_CursoId",
                table: "Aulas",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "UQ_TITULO_CURSO_AULAS",
                table: "Aulas",
                columns: new[] { "Titulo", "CursoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_NOME_CURSOS",
                table: "Cursos",
                column: "Nome",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aulas");

            migrationBuilder.DropTable(
                name: "Cursos");
        }
    }
}
