using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Portal.Data.Migrations
{
    public partial class ChangeUserIdToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserIssues_AspNetUsers_UserId1",
                table: "UserIssues");

            migrationBuilder.DropIndex(
                name: "IX_UserIssues_UserId1",
                table: "UserIssues");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserIssues");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserIssues",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_UserIssues_UserId",
                table: "UserIssues",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserIssues_AspNetUsers_UserId",
                table: "UserIssues",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserIssues_AspNetUsers_UserId",
                table: "UserIssues");

            migrationBuilder.DropIndex(
                name: "IX_UserIssues_UserId",
                table: "UserIssues");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserIssues",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserIssues",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserIssues_UserId1",
                table: "UserIssues",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserIssues_AspNetUsers_UserId1",
                table: "UserIssues",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
