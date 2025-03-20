using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace users_directory.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonRelationships_People_UserId",
                table: "PersonRelationships");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumbers_People_PersonId",
                table: "PhoneNumbers");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonRelationships_People_UserId",
                table: "PersonRelationships",
                column: "UserId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumbers_People_PersonId",
                table: "PhoneNumbers",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonRelationships_People_UserId",
                table: "PersonRelationships");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumbers_People_PersonId",
                table: "PhoneNumbers");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonRelationships_People_UserId",
                table: "PersonRelationships",
                column: "UserId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumbers_People_PersonId",
                table: "PhoneNumbers",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
