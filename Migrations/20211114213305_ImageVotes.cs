using Microsoft.EntityFrameworkCore.Migrations;

namespace AplikacjaMVC.Migrations
{
    public partial class ImageVotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            //migrationBuilder.CreateTable(
            //    name: "Images",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Title = table.Column<string>(type: "nvarchar(50)", nullable: true),
            //        Description = table.Column<string>(type: "nvarchar(100)", nullable: true),
            //        Filename = table.Column<string>(type: "nvarchar(100)", nullable: true),
            //        Filetype = table.Column<string>(type: "nvarchar(10)", nullable: true),
            //        Path = table.Column<string>(type: "nvarchar(50)", nullable: true),
            //        Rating = table.Column<int>(type: "int", nullable: false),
            //        CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Images", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Register",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Hash = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Salt = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Register", x => x.Id);
            //    });

            migrationBuilder.CreateTable(
                name: "ImagesVotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagesId = table.Column<int>(type: "int", nullable: true),
                    RegisterId = table.Column<int>(type: "int", nullable: true),
                    Vote = table.Column<string>(type: "nvarchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagesVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImagesVotes_Images_ImagesId",
                        column: x => x.ImagesId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImagesVotes_Register_RegisterId",
                        column: x => x.RegisterId,
                        principalTable: "Register",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImagesVotes_ImagesId",
                table: "ImagesVotes",
                column: "ImagesId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagesVotes_RegisterId",
                table: "ImagesVotes",
                column: "RegisterId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
              name: "ImagesVotes");

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
    }
}
