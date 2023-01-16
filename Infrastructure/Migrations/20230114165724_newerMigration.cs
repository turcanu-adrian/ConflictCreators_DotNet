using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class newerMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prompts_PromptSets_PromptSetId",
                table: "Prompts");

            migrationBuilder.AddForeignKey(
                name: "FK_Prompts_PromptSets_PromptSetId",
                table: "Prompts",
                column: "PromptSetId",
                principalTable: "PromptSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prompts_PromptSets_PromptSetId",
                table: "Prompts");

            migrationBuilder.AddForeignKey(
                name: "FK_Prompts_PromptSets_PromptSetId",
                table: "Prompts",
                column: "PromptSetId",
                principalTable: "PromptSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
