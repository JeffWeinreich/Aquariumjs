using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Angular.Web.Migrations
{
    public partial class TankOwnerMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Owner",
                table: "Tanks",
                newName: "OwnerId");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Tanks",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tanks_OwnerId",
                table: "Tanks",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tanks_Users_OwnerId",
                table: "Tanks",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tanks_Users_OwnerId",
                table: "Tanks");

            migrationBuilder.DropIndex(
                name: "IX_Tanks_OwnerId",
                table: "Tanks");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Tanks",
                newName: "Owner");

            migrationBuilder.AlterColumn<string>(
                name: "Owner",
                table: "Tanks",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
