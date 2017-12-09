using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Portal.Data.Migrations
{
    public partial class addIssueId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueState_Issues_IssueId",
                table: "IssueState");

            migrationBuilder.AlterColumn<int>(
                name: "IssueId",
                table: "IssueState",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_IssueState_Issues_IssueId",
                table: "IssueState",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueState_Issues_IssueId",
                table: "IssueState");

            migrationBuilder.AlterColumn<int>(
                name: "IssueId",
                table: "IssueState",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_IssueState_Issues_IssueId",
                table: "IssueState",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
