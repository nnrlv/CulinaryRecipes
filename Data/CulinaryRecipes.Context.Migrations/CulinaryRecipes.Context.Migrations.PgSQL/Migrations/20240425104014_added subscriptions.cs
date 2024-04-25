using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CulinaryRecipes.Context.Migrations.PgSQL.Migrations
{
    /// <inheritdoc />
    public partial class addedsubscriptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "recipe_subscription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubscriberId = table.Column<int>(type: "integer", nullable: false),
                    RecipeId = table.Column<int>(type: "integer", nullable: false),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipe_subscription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_recipe_subscription_recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "recipes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_recipe_subscription_users_SubscriberId",
                        column: x => x.SubscriberId,
                        principalTable: "users",
                        principalColumn: "EntryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubscriberId = table.Column<int>(type: "integer", nullable: false),
                    SubscriberId1 = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorId = table.Column<int>(type: "integer", nullable: false),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_subscriptions_users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "users",
                        principalColumn: "EntryId");
                    table.ForeignKey(
                        name: "FK_user_subscriptions_users_SubscriberId1",
                        column: x => x.SubscriberId1,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_recipe_subscription_RecipeId",
                table: "recipe_subscription",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_recipe_subscription_SubscriberId",
                table: "recipe_subscription",
                column: "SubscriberId");

            migrationBuilder.CreateIndex(
                name: "IX_recipe_subscription_Uid",
                table: "recipe_subscription",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_subscriptions_AuthorId",
                table: "user_subscriptions",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_user_subscriptions_SubscriberId1",
                table: "user_subscriptions",
                column: "SubscriberId1");

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
                name: "recipe_subscription");

            migrationBuilder.DropTable(
                name: "user_subscriptions");
        }
    }
}
