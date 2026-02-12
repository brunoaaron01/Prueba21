using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prueba21.Migrations
{
    /// <inheritdoc />
    public partial class AgregarAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Personal",
                columns: new[] { "PersonalId", "Contrasenia", "Email", "Nombre", "Rol" },
                values: new object[] { 1, "admin", "admin@gmail.com", "Administrador", "Administrador" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Personal",
                keyColumn: "PersonalId",
                keyValue: 1);
        }
    }
}
