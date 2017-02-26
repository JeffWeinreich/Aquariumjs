using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Angular.Web.Migrations
{
    public partial class TankMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tank",
                table: "Fishes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TankId",
                table: "Fishes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tanks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Owner = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tanks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fishes_TankId",
                table: "Fishes",
                column: "TankId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fishes_Tanks_TankId",
                table: "Fishes",
                column: "TankId",
                principalTable: "Tanks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fishes_Tanks_TankId",
                table: "Fishes");

            migrationBuilder.DropTable(
                name: "Tanks");

            migrationBuilder.DropIndex(
                name: "IX_Fishes_TankId",
                table: "Fishes");

            migrationBuilder.DropColumn(
                name: "Tank",
                table: "Fishes");

            migrationBuilder.DropColumn(
                name: "TankId",
                table: "Fishes");
        }
    }
}
