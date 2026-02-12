using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prueba21.Migrations
{
    /// <inheritdoc />
    public partial class ActualizacionBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(101)", maxLength: 101, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ClienteId);
                });

            migrationBuilder.CreateTable(
                name: "FormasDePago",
                columns: table => new
                {
                    FormaDePagoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormasDePago", x => x.FormaDePagoId);
                });

            migrationBuilder.CreateTable(
                name: "Habitaciones",
                columns: table => new
                {
                    HabitacionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habitaciones", x => x.HabitacionId);
                });

            migrationBuilder.CreateTable(
                name: "Personal",
                columns: table => new
                {
                    PersonalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contrasenia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personal", x => x.PersonalId);
                });

            migrationBuilder.CreateTable(
                name: "OrdenesReserva",
                columns: table => new
                {
                    OrdenReservaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    HabitacionId = table.Column<int>(type: "int", nullable: false),
                    FormaDePagoId = table.Column<int>(type: "int", nullable: false),
                    FechaEntrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaSalida = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenesReserva", x => x.OrdenReservaId);
                    table.ForeignKey(
                        name: "FK_OrdenesReserva_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdenesReserva_FormasDePago_FormaDePagoId",
                        column: x => x.FormaDePagoId,
                        principalTable: "FormasDePago",
                        principalColumn: "FormaDePagoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdenesReserva_Habitaciones_HabitacionId",
                        column: x => x.HabitacionId,
                        principalTable: "Habitaciones",
                        principalColumn: "HabitacionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdenesConserjeria",
                columns: table => new
                {
                    OrdenConserjeriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HabitacionId = table.Column<int>(type: "int", nullable: false),
                    PersonalId = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenesConserjeria", x => x.OrdenConserjeriaId);
                    table.ForeignKey(
                        name: "FK_OrdenesConserjeria_Habitaciones_HabitacionId",
                        column: x => x.HabitacionId,
                        principalTable: "Habitaciones",
                        principalColumn: "HabitacionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdenesConserjeria_Personal_PersonalId",
                        column: x => x.PersonalId,
                        principalTable: "Personal",
                        principalColumn: "PersonalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdenesHospedaje",
                columns: table => new
                {
                    OrdenHospedajeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdenReservaId = table.Column<int>(type: "int", nullable: true),
                    ClienteId = table.Column<int>(type: "int", nullable: true),
                    HabitacionId = table.Column<int>(type: "int", nullable: true),
                    FormaDePagoId = table.Column<int>(type: "int", nullable: false),
                    FechaCheckIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaCheckOut = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenesHospedaje", x => x.OrdenHospedajeId);
                    table.CheckConstraint("CHK_OrdenHospedaje_Tipo", "(OrdenReservaId IS NOT NULL) OR (ClienteId IS NOT NULL AND HabitacionId IS NOT NULL)");
                    table.ForeignKey(
                        name: "FK_OrdenesHospedaje_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "ClienteId");
                    table.ForeignKey(
                        name: "FK_OrdenesHospedaje_FormasDePago_FormaDePagoId",
                        column: x => x.FormaDePagoId,
                        principalTable: "FormasDePago",
                        principalColumn: "FormaDePagoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdenesHospedaje_Habitaciones_HabitacionId",
                        column: x => x.HabitacionId,
                        principalTable: "Habitaciones",
                        principalColumn: "HabitacionId");
                    table.ForeignKey(
                        name: "FK_OrdenesHospedaje_OrdenesReserva_OrdenReservaId",
                        column: x => x.OrdenReservaId,
                        principalTable: "OrdenesReserva",
                        principalColumn: "OrdenReservaId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesConserjeria_HabitacionId",
                table: "OrdenesConserjeria",
                column: "HabitacionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesConserjeria_PersonalId",
                table: "OrdenesConserjeria",
                column: "PersonalId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesHospedaje_ClienteId",
                table: "OrdenesHospedaje",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesHospedaje_FormaDePagoId",
                table: "OrdenesHospedaje",
                column: "FormaDePagoId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesHospedaje_HabitacionId",
                table: "OrdenesHospedaje",
                column: "HabitacionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesHospedaje_OrdenReservaId",
                table: "OrdenesHospedaje",
                column: "OrdenReservaId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesReserva_ClienteId",
                table: "OrdenesReserva",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesReserva_FormaDePagoId",
                table: "OrdenesReserva",
                column: "FormaDePagoId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesReserva_HabitacionId",
                table: "OrdenesReserva",
                column: "HabitacionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdenesConserjeria");

            migrationBuilder.DropTable(
                name: "OrdenesHospedaje");

            migrationBuilder.DropTable(
                name: "Personal");

            migrationBuilder.DropTable(
                name: "OrdenesReserva");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "FormasDePago");

            migrationBuilder.DropTable(
                name: "Habitaciones");
        }
    }
}
