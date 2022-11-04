using Microsoft.EntityFrameworkCore.Migrations;

namespace KnowledgeSpace.BackendServer.Data.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_UserId",
                table: "Votes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_KnowledgeBaseId",
                table: "Reports",
                column: "KnowledgeBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ReportUserId",
                table: "Reports",
                column: "ReportUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_CommandId",
                table: "Permissions",
                column: "CommandId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_FunctionId",
                table: "Permissions",
                column: "FunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_LabelInKnowledgeBases_KnowledgeBaseId",
                table: "LabelInKnowledgeBases",
                column: "KnowledgeBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeBases_CategoryId",
                table: "KnowledgeBases",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_KnowledgeBaseId",
                table: "Comments",
                column: "KnowledgeBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_OwnerUserId",
                table: "Comments",
                column: "OwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandInFunctions_FunctionId",
                table: "CommandInFunctions",
                column: "FunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_KnowledgeBaseId",
                table: "Attachments",
                column: "KnowledgeBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_UserId",
                table: "ActivityLogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLogs_AspNetUsers_UserId",
                table: "ActivityLogs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_KnowledgeBases_KnowledgeBaseId",
                table: "Attachments",
                column: "KnowledgeBaseId",
                principalTable: "KnowledgeBases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommandInFunctions_Commands_CommandId",
                table: "CommandInFunctions",
                column: "CommandId",
                principalTable: "Commands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommandInFunctions_Functions_FunctionId",
                table: "CommandInFunctions",
                column: "FunctionId",
                principalTable: "Functions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_OwnerUserId",
                table: "Comments",
                column: "OwnerUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_KnowledgeBases_KnowledgeBaseId",
                table: "Comments",
                column: "KnowledgeBaseId",
                principalTable: "KnowledgeBases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KnowledgeBases_Categories_CategoryId",
                table: "KnowledgeBases",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LabelInKnowledgeBases_KnowledgeBases_KnowledgeBaseId",
                table: "LabelInKnowledgeBases",
                column: "KnowledgeBaseId",
                principalTable: "KnowledgeBases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LabelInKnowledgeBases_Labels_LabelId",
                table: "LabelInKnowledgeBases",
                column: "LabelId",
                principalTable: "Labels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_AspNetRoles_RoleId",
                table: "Permissions",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Commands_CommandId",
                table: "Permissions",
                column: "CommandId",
                principalTable: "Commands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Functions_FunctionId",
                table: "Permissions",
                column: "FunctionId",
                principalTable: "Functions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_AspNetUsers_ReportUserId",
                table: "Reports",
                column: "ReportUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_KnowledgeBases_KnowledgeBaseId",
                table: "Reports",
                column: "KnowledgeBaseId",
                principalTable: "KnowledgeBases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_AspNetUsers_UserId",
                table: "Votes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_KnowledgeBases_KnowledgeBaseId",
                table: "Votes",
                column: "KnowledgeBaseId",
                principalTable: "KnowledgeBases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLogs_AspNetUsers_UserId",
                table: "ActivityLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_KnowledgeBases_KnowledgeBaseId",
                table: "Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_CommandInFunctions_Commands_CommandId",
                table: "CommandInFunctions");

            migrationBuilder.DropForeignKey(
                name: "FK_CommandInFunctions_Functions_FunctionId",
                table: "CommandInFunctions");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_OwnerUserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_KnowledgeBases_KnowledgeBaseId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_KnowledgeBases_Categories_CategoryId",
                table: "KnowledgeBases");

            migrationBuilder.DropForeignKey(
                name: "FK_LabelInKnowledgeBases_KnowledgeBases_KnowledgeBaseId",
                table: "LabelInKnowledgeBases");

            migrationBuilder.DropForeignKey(
                name: "FK_LabelInKnowledgeBases_Labels_LabelId",
                table: "LabelInKnowledgeBases");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_AspNetRoles_RoleId",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Commands_CommandId",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Functions_FunctionId",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_AspNetUsers_ReportUserId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_KnowledgeBases_KnowledgeBaseId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_AspNetUsers_UserId",
                table: "Votes");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_KnowledgeBases_KnowledgeBaseId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_UserId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Reports_KnowledgeBaseId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_ReportUserId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_CommandId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_FunctionId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_LabelInKnowledgeBases_KnowledgeBaseId",
                table: "LabelInKnowledgeBases");

            migrationBuilder.DropIndex(
                name: "IX_KnowledgeBases_CategoryId",
                table: "KnowledgeBases");

            migrationBuilder.DropIndex(
                name: "IX_Comments_KnowledgeBaseId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_OwnerUserId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_CommandInFunctions_FunctionId",
                table: "CommandInFunctions");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_KnowledgeBaseId",
                table: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_ActivityLogs_UserId",
                table: "ActivityLogs");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");
        }
    }
}
