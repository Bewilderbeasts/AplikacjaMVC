using Microsoft.EntityFrameworkCore.Migrations;

namespace AplikacjaMVC.Migrations
{
    public partial class ImagesVotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageVotes_Images_ImagesId",
                table: "ImageVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_ImageVotes_Register_RegisterId",
                table: "ImageVotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageVotes",
                table: "ImageVotes");

            migrationBuilder.RenameTable(
                name: "ImageVotes",
                newName: "ImagesVotes");

            migrationBuilder.RenameIndex(
                name: "IX_ImageVotes_RegisterId",
                table: "ImagesVotes",
                newName: "IX_ImagesVotes_RegisterId");

            migrationBuilder.RenameIndex(
                name: "IX_ImageVotes_ImagesId",
                table: "ImagesVotes",
                newName: "IX_ImagesVotes_ImagesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImagesVotes",
                table: "ImagesVotes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagesVotes_Images_ImagesId",
                table: "ImagesVotes",
                column: "ImagesId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ImagesVotes_Register_RegisterId",
                table: "ImagesVotes",
                column: "RegisterId",
                principalTable: "Register",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImagesVotes_Images_ImagesId",
                table: "ImagesVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_ImagesVotes_Register_RegisterId",
                table: "ImagesVotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImagesVotes",
                table: "ImagesVotes");

            migrationBuilder.RenameTable(
                name: "ImagesVotes",
                newName: "ImageVotes");

            migrationBuilder.RenameIndex(
                name: "IX_ImagesVotes_RegisterId",
                table: "ImageVotes",
                newName: "IX_ImageVotes_RegisterId");

            migrationBuilder.RenameIndex(
                name: "IX_ImagesVotes_ImagesId",
                table: "ImageVotes",
                newName: "IX_ImageVotes_ImagesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImageVotes",
                table: "ImageVotes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageVotes_Images_ImagesId",
                table: "ImageVotes",
                column: "ImagesId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ImageVotes_Register_RegisterId",
                table: "ImageVotes",
                column: "RegisterId",
                principalTable: "Register",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
