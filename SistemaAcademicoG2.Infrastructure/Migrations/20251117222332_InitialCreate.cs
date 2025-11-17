using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaAcademicoG2.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_Asignatura",
                columns: table => new
                {
                    IdAsignatura = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_Asignatura", x => x.IdAsignatura);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_Grado",
                columns: table => new
                {
                    idGrado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    estado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_Grado", x => x.idGrado);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_Periodo",
                columns: table => new
                {
                    IdPeriodo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaInicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Estado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_Periodo", x => x.IdPeriodo);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_Rol",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_Rol", x => x.IdRol);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "T_GradoAsignatura",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GradoId = table.Column<int>(type: "int", nullable: false),
                    AsignaturaId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GradoAsignatura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_GradoAsignatura_t_Asignatura_AsignaturaId",
                        column: x => x.AsignaturaId,
                        principalTable: "t_Asignatura",
                        principalColumn: "IdAsignatura",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_GradoAsignatura_t_Grado_GradoId",
                        column: x => x.GradoId,
                        principalTable: "t_Grado",
                        principalColumn: "idGrado",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdRol = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Apellido = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Correo = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Clave = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_Usuario", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_t_Usuario_t_Rol_IdRol",
                        column: x => x.IdRol,
                        principalTable: "t_Rol",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_docenteasignaturagrado",
                columns: table => new
                {
                    IdDGA = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdGrado = table.Column<int>(type: "int", nullable: false),
                    IdAsignatura = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    GradoAsignaturaIdGradoAsignatura = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_docenteasignaturagrado", x => x.IdDGA);
                    table.ForeignKey(
                        name: "FK_t_docenteasignaturagrado_T_GradoAsignatura_GradoAsignaturaId~",
                        column: x => x.GradoAsignaturaIdGradoAsignatura,
                        principalTable: "T_GradoAsignatura",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_t_docenteasignaturagrado_t_Asignatura_IdAsignatura",
                        column: x => x.IdAsignatura,
                        principalTable: "t_Asignatura",
                        principalColumn: "IdAsignatura",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_docenteasignaturagrado_t_Grado_IdGrado",
                        column: x => x.IdGrado,
                        principalTable: "t_Grado",
                        principalColumn: "idGrado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_docenteasignaturagrado_t_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "t_Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_Inscripcion",
                columns: table => new
                {
                    idInscripcion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    idGrado = table.Column<int>(type: "int", nullable: false),
                    fechaIngreso = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    anioLectivo = table.Column<int>(type: "int", nullable: false),
                    estado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_Inscripcion", x => x.idInscripcion);
                    table.ForeignKey(
                        name: "FK_t_Inscripcion_t_Grado_idGrado",
                        column: x => x.idGrado,
                        principalTable: "t_Grado",
                        principalColumn: "idGrado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_Inscripcion_t_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "t_Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_nota",
                columns: table => new
                {
                    IdNota = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdAsignatura = table.Column<int>(type: "int", nullable: false),
                    IdPeriodo = table.Column<int>(type: "int", nullable: false),
                    Nota1 = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Nota2 = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Nota3 = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    PromedioFinal = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    EstadoAcademico = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_nota", x => x.IdNota);
                    table.ForeignKey(
                        name: "FK_t_nota_t_Asignatura_IdAsignatura",
                        column: x => x.IdAsignatura,
                        principalTable: "t_Asignatura",
                        principalColumn: "IdAsignatura",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_nota_t_Periodo_IdPeriodo",
                        column: x => x.IdPeriodo,
                        principalTable: "t_Periodo",
                        principalColumn: "IdPeriodo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_nota_t_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "t_Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_Asistencia",
                columns: table => new
                {
                    IdAsistencia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdDGA = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EstadoAsistencia = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Observacion = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_Asistencia", x => x.IdAsistencia);
                    table.ForeignKey(
                        name: "FK_t_Asistencia_t_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "t_Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_Asistencia_t_docenteasignaturagrado_IdDGA",
                        column: x => x.IdDGA,
                        principalTable: "t_docenteasignaturagrado",
                        principalColumn: "IdDGA",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_t_Asistencia_IdDGA",
                table: "t_Asistencia",
                column: "IdDGA");

            migrationBuilder.CreateIndex(
                name: "IX_t_Asistencia_IdUsuario",
                table: "t_Asistencia",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_t_docenteasignaturagrado_GradoAsignaturaIdGradoAsignatura",
                table: "t_docenteasignaturagrado",
                column: "GradoAsignaturaIdGradoAsignatura");

            migrationBuilder.CreateIndex(
                name: "IX_t_docenteasignaturagrado_IdAsignatura",
                table: "t_docenteasignaturagrado",
                column: "IdAsignatura");

            migrationBuilder.CreateIndex(
                name: "IX_t_docenteasignaturagrado_IdGrado",
                table: "t_docenteasignaturagrado",
                column: "IdGrado");

            migrationBuilder.CreateIndex(
                name: "IX_t_docenteasignaturagrado_IdUsuario",
                table: "t_docenteasignaturagrado",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_T_GradoAsignatura_AsignaturaId",
                table: "T_GradoAsignatura",
                column: "AsignaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_T_GradoAsignatura_GradoId",
                table: "T_GradoAsignatura",
                column: "GradoId");

            migrationBuilder.CreateIndex(
                name: "IX_t_Inscripcion_idGrado",
                table: "t_Inscripcion",
                column: "idGrado");

            migrationBuilder.CreateIndex(
                name: "IX_t_Inscripcion_IdUsuario",
                table: "t_Inscripcion",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_t_nota_IdAsignatura",
                table: "t_nota",
                column: "IdAsignatura");

            migrationBuilder.CreateIndex(
                name: "IX_t_nota_IdPeriodo",
                table: "t_nota",
                column: "IdPeriodo");

            migrationBuilder.CreateIndex(
                name: "IX_t_nota_IdUsuario",
                table: "t_nota",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_t_Usuario_IdRol",
                table: "t_Usuario",
                column: "IdRol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_Asistencia");

            migrationBuilder.DropTable(
                name: "t_Inscripcion");

            migrationBuilder.DropTable(
                name: "t_nota");

            migrationBuilder.DropTable(
                name: "t_docenteasignaturagrado");

            migrationBuilder.DropTable(
                name: "t_Periodo");

            migrationBuilder.DropTable(
                name: "T_GradoAsignatura");

            migrationBuilder.DropTable(
                name: "t_Usuario");

            migrationBuilder.DropTable(
                name: "t_Asignatura");

            migrationBuilder.DropTable(
                name: "t_Grado");

            migrationBuilder.DropTable(
                name: "t_Rol");
        }
    }
}
