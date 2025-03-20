using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace users_directory.Migrations
{
    /// <inheritdoc />
    public partial class UsersMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GendersType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GendersType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumbersType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumbersType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RelationshipTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationshipTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: false),
                    PersonalNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    ProfileImage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                    table.ForeignKey(
                        name: "FK_People_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_People_GendersType_GenderId",
                        column: x => x.GenderId,
                        principalTable: "GendersType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonRelationships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RelatedTypeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RelatedPerson = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonRelationships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonRelationships_People_UserId",
                        column: x => x.UserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonRelationships_RelationshipTypes_RelatedTypeId",
                        column: x => x.RelatedTypeId,
                        principalTable: "RelationshipTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhoneNumbers_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhoneNumbers_PhoneNumbersType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "PhoneNumbersType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CityName" },
                values: new object[,]
                {
                    { 1, "თბილისი" },
                    { 2, "რუსთავი" },
                    { 3, "ქუთაისი" },
                    { 4, "თელავი" },
                    { 5, "ბათუმი" },
                    { 6, "მარნეული" },
                    { 7, "ზუგდიდი" },
                    { 8, "ოზურგეთი" },
                    { 9, "ქობულეთი" },
                    { 10, "ყვარელი" }
                });

            migrationBuilder.InsertData(
                table: "GendersType",
                columns: new[] { "Id", "Gender" },
                values: new object[,]
                {
                    { 1, "კაცი" },
                    { 2, "ქალი" },
                    { 3, "სხვა" }
                });

            migrationBuilder.InsertData(
                table: "PhoneNumbersType",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "მობილური" },
                    { 2, "სახლის" },
                    { 3, "ოფისის" }
                });

            migrationBuilder.InsertData(
                table: "RelationshipTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "ნათესავი" },
                    { 2, "კოლეგა" },
                    { 3, "ნაცნობი" },
                    { 4, "სხვა" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_People_CityId",
                table: "People",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_People_GenderId",
                table: "People",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_People_PersonalNumber",
                table: "People",
                column: "PersonalNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonRelationships_RelatedTypeId",
                table: "PersonRelationships",
                column: "RelatedTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonRelationships_UserId",
                table: "PersonRelationships",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumbers_PersonId",
                table: "PhoneNumbers",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumbers_TypeId",
                table: "PhoneNumbers",
                column: "TypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonRelationships");

            migrationBuilder.DropTable(
                name: "PhoneNumbers");

            migrationBuilder.DropTable(
                name: "RelationshipTypes");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "PhoneNumbersType");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "GendersType");
        }
    }
}
