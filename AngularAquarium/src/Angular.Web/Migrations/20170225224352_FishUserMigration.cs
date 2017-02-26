using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Angular.Web.Migrations
{
    public partial class FishUserMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Owner",
                table: "Fishes",
                newName: "OwnerId");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Fishes",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fishes_OwnerId",
                table: "Fishes",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fishes_Users_OwnerId",
                table: "Fishes",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fishes_Users_OwnerId",
                table: "Fishes");

            migrationBuilder.DropIndex(
                name: "IX_Fishes_OwnerId",
                table: "Fishes");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Fishes",
                newName: "Owner");

            migrationBuilder.AlterColumn<string>(
                name: "Owner",
                table: "Fishes",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
