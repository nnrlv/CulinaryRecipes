using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CulinaryRecipes.Context.Migrations.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class addedcommentsandsubscriptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_comments_recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comments_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "EntryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "recipe_subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriberId = table.Column<int>(type: "int", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipe_subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_recipe_subscriptions_recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "recipes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_recipe_subscriptions_users_SubscriberId",
                        column: x => x.SubscriberId,
                        principalTable: "users",
                        principalColumn: "EntryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriberId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_subscriptions_users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "users",
                        principalColumn: "EntryId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_comments_RecipeId",
                table: "comments",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_comments_Uid",
                table: "comments",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_comments_UserId",
                table: "comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_recipe_subscriptions_RecipeId",
                table: "recipe_subscriptions",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_recipe_subscriptions_SubscriberId",
                table: "recipe_subscriptions",
                column: "SubscriberId");

            migrationBuilder.CreateIndex(
                name: "IX_recipe_subscriptions_Uid",
                table: "recipe_subscriptions",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_subscriptions_AuthorId",
                table: "user_subscriptions",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_user_subscriptions_Uid",
                table: "user_subscriptions",
                column: "Uid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "recipe_subscriptions");

            migrationBuilder.DropTable(
                name: "user_subscriptions");
        }
    }
}
