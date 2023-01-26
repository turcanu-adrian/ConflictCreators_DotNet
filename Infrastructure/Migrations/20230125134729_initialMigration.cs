using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AchievementPoints = table.Column<int>(type: "int", nullable: false),
                    FastestRun = table.Column<TimeSpan>(type: "time", nullable: false),
                    currentAvatar = table.Column<int>(type: "int", nullable: false),
                    GamesWon = table.Column<int>(type: "int", nullable: false),
                    Badges = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromptSets",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromptSets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromptSets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Prompts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PromptSetId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrectAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WrongAnswers = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prompts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prompts_PromptSets_PromptSetId",
                        column: x => x.PromptSetId,
                        principalTable: "PromptSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PromptSets",
                columns: new[] { "Id", "CreatedByUserId", "Name", "Tags", "UserId" },
                values: new object[] { "fdd1fd6f2aa24d29a1a6fb710df9f033", "default", "Default Prompt Set", "[\"default\",\"basic\"]", null });

            migrationBuilder.InsertData(
                table: "Prompts",
                columns: new[] { "Id", "CorrectAnswer", "PromptSetId", "Question", "WrongAnswers" },
                values: new object[,]
                {
                    { "024e9f4d402144aabf670c948eec49f0", "Bucharest", "fdd1fd6f2aa24d29a1a6fb710df9f033", "What is the capital of Romania?", "[\"Paris\",\"Rome\",\"Texas\"]" },
                    { "0bface1bcae847a6826f6a8e408de9fa", "8", "fdd1fd6f2aa24d29a1a6fb710df9f033", "How many sides does an Octagon have?", "[\"4\",\"6\",\"10\"]" },
                    { "0d15de8340594103977491d0a22bf94f", "Clownfish", "fdd1fd6f2aa24d29a1a6fb710df9f033", "Which type of fish is Nemo?", "[\"Swordfish\",\"Sailfish\",\"Whale\"]" },
                    { "25fce8d270c74d0586a2c9ff32f76920", "4", "fdd1fd6f2aa24d29a1a6fb710df9f033", "How many legs does a sheep have?", "[\"2\",\"3\",\"4\"]" },
                    { "2bb7a830c400473ab940d4331b0e6321", "Chiuaua", "fdd1fd6f2aa24d29a1a6fb710df9f033", "What is the smallest breed of dog?", "[\"Golden Retriever\",\"Bulldog\",\"Pitbull\"]" },
                    { "320f2aabb05747f89e7e741585652772", "1000 grams", "fdd1fd6f2aa24d29a1a6fb710df9f033", "How many grams makes 1 kilogram?", "[\"10 grams\",\"100 grams\",\"1 gram\"]" },
                    { "ae99acb2348a4be49fae3e19198f1e56", "Green", "fdd1fd6f2aa24d29a1a6fb710df9f033", "What color is an emerald?", "[\"Golden\",\"Black\",\"Pink\"]" },
                    { "b9bf8ab2c8d04ea680921ccfeff9a0ac", "Yellow", "fdd1fd6f2aa24d29a1a6fb710df9f033", "What color is the sun?", "[\"Green\",\"Red\",\"Blue\",\"Orange\"]" },
                    { "bfddf20ebe974a77973a2ad920dc042c", "Asia", "fdd1fd6f2aa24d29a1a6fb710df9f033", "Which is the largest continent?", "[\"Europe\",\"North America\",\"Africa\"]" },
                    { "c02bbe8b4ef24b01a6a92609da732bb9", "Paris", "fdd1fd6f2aa24d29a1a6fb710df9f033", "What is the capital of France?", "[\"Bucharest\",\"Rome\",\"Moscow\"]" },
                    { "c826b8560f384f898b709bd5082d1c83", "January", "fdd1fd6f2aa24d29a1a6fb710df9f033", "What month has the internship ended in?", "[\"December\",\"October\",\"February\"]" },
                    { "d01f6e51a95949d787dbdc0e9c1e01a3", "50", "fdd1fd6f2aa24d29a1a6fb710df9f033", "How many states are in the United States?", "[\"40\",\"45\",\"52\"]" },
                    { "e8d55525ab644652910b2ac03a5bf06b", "Bald Eagle", "fdd1fd6f2aa24d29a1a6fb710df9f033", "What is the National Animal of the USA?", "[\"Black Bear\",\"Grizzly Bear\",\"American Bison\"]" },
                    { "e97187ab96a4434e8b2df87372c7b9a7", "Parrot", "fdd1fd6f2aa24d29a1a6fb710df9f033", "Which bird can mimic humans?", "[\"Eagle\",\"Seagull\",\"Pelican\"]" },
                    { "ed1d641e06c64286a47aff6038340f0b", "Honey", "fdd1fd6f2aa24d29a1a6fb710df9f033", "What do bees produce?", "[\"Milk\",\"Flowers\",\"Water\"]" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Prompts_PromptSetId",
                table: "Prompts",
                column: "PromptSetId");

            migrationBuilder.CreateIndex(
                name: "IX_PromptSets_UserId",
                table: "PromptSets",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Prompts");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "PromptSets");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
