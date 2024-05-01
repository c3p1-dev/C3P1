using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace C3P1.Migrations.VisualCarnet
{
    /// <inheritdoc />
    public partial class CreateVisualCarnetSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FicCpts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CodCpt = table.Column<string>(type: "TEXT", maxLength: 6, nullable: false),
                    Nom = table.Column<string>(type: "TEXT", maxLength: 35, nullable: false),
                    Visible = table.Column<string>(type: "TEXT", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FicCpts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FicFams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CodFam = table.Column<string>(type: "TEXT", maxLength: 6, nullable: false),
                    Nom = table.Column<string>(type: "TEXT", maxLength: 35, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FicFams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FicSfas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CodSfa = table.Column<string>(type: "TEXT", maxLength: 6, nullable: false),
                    Nom = table.Column<string>(type: "TEXT", maxLength: 35, nullable: true),
                    CodFam = table.Column<string>(type: "TEXT", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FicSfas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WrkEcrLigs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CodCpt = table.Column<string>(type: "TEXT", maxLength: 6, nullable: false),
                    Nol = table.Column<int>(type: "INTEGER", nullable: false),
                    Jma = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    JmaVal = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    NoChq = table.Column<string>(type: "TEXT", maxLength: 7, nullable: true),
                    Lib1 = table.Column<string>(type: "TEXT", maxLength: 60, nullable: true),
                    Lib2 = table.Column<string>(type: "TEXT", maxLength: 60, nullable: true),
                    Deb = table.Column<double>(type: "REAL", nullable: true),
                    Cre = table.Column<double>(type: "REAL", nullable: true),
                    CodSfa = table.Column<string>(type: "TEXT", maxLength: 6, nullable: false),
                    SldProgressif = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WrkEcrLigs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FicCpts");

            migrationBuilder.DropTable(
                name: "FicFams");

            migrationBuilder.DropTable(
                name: "FicSfas");

            migrationBuilder.DropTable(
                name: "WrkEcrLigs");
        }
    }
}
