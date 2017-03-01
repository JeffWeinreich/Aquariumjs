using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Angular.Web.Migrations
{
    public partial class AddsFishToTanksMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fishes_Tanks_TankId",
                table: "Fishes");

            migrationBuilder.DropColumn(
                name: "Tank",
                table: "Fishes");

            migrationBuilder.AlterColumn<int>(
                name: "TankId",
                table: "Fishes",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Fishes_Tanks_TankId",
                table: "Fishes",
                column: "TankId",
                principalTable: "Tanks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fishes_Tanks_TankId",
                table: "Fishes");

            migrationBuilder.AlterColumn<int>(
                name: "TankId",
                table: "Fishes",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Tank",
                table: "Fishes",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Fishes_Tanks_TankId",
                table: "Fishes",
                column: "TankId",
                principalTable: "Tanks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
