using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CulinaryRecipes.Context.Migrations.PgSQL.Migrations
{
    /// <inheritdoc />
    public partial class modifiedusersubscriptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recipe_subscription_recipes_RecipeId",
                table: "recipe_subscription");

            migrationBuilder.DropForeignKey(
                name: "FK_recipe_subscription_users_SubscriberId",
                table: "recipe_subscription");

            migrationBuilder.DropForeignKey(
                name: "FK_user_subscriptions_users_SubscriberId1",
                table: "user_subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_user_subscriptions_SubscriberId1",
                table: "user_subscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_recipe_subscription",
                table: "recipe_subscription");

            migrationBuilder.DropColumn(
                name: "SubscriberId1",
                table: "user_subscriptions");

            migrationBuilder.RenameTable(
                name: "recipe_subscription",
                newName: "recipe_subscriptions");

            migrationBuilder.RenameIndex(
                name: "IX_recipe_subscription_Uid",
                table: "recipe_subscriptions",
                newName: "IX_recipe_subscriptions_Uid");

            migrationBuilder.RenameIndex(
                name: "IX_recipe_subscription_SubscriberId",
                table: "recipe_subscriptions",
                newName: "IX_recipe_subscriptions_SubscriberId");

            migrationBuilder.RenameIndex(
                name: "IX_recipe_subscription_RecipeId",
                table: "recipe_subscriptions",
                newName: "IX_recipe_subscriptions_RecipeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_recipe_subscriptions",
                table: "recipe_subscriptions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_recipe_subscriptions_recipes_RecipeId",
                table: "recipe_subscriptions",
                column: "RecipeId",
                principalTable: "recipes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_recipe_subscriptions_users_SubscriberId",
                table: "recipe_subscriptions",
                column: "SubscriberId",
                principalTable: "users",
                principalColumn: "EntryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recipe_subscriptions_recipes_RecipeId",
                table: "recipe_subscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_recipe_subscriptions_users_SubscriberId",
                table: "recipe_subscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_recipe_subscriptions",
                table: "recipe_subscriptions");

            migrationBuilder.RenameTable(
                name: "recipe_subscriptions",
                newName: "recipe_subscription");

            migrationBuilder.RenameIndex(
                name: "IX_recipe_subscriptions_Uid",
                table: "recipe_subscription",
                newName: "IX_recipe_subscription_Uid");

            migrationBuilder.RenameIndex(
                name: "IX_recipe_subscriptions_SubscriberId",
                table: "recipe_subscription",
                newName: "IX_recipe_subscription_SubscriberId");

            migrationBuilder.RenameIndex(
                name: "IX_recipe_subscriptions_RecipeId",
                table: "recipe_subscription",
                newName: "IX_recipe_subscription_RecipeId");

            migrationBuilder.AddColumn<Guid>(
                name: "SubscriberId1",
                table: "user_subscriptions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_recipe_subscription",
                table: "recipe_subscription",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_user_subscriptions_SubscriberId1",
                table: "user_subscriptions",
                column: "SubscriberId1");

            migrationBuilder.AddForeignKey(
                name: "FK_recipe_subscription_recipes_RecipeId",
                table: "recipe_subscription",
                column: "RecipeId",
                principalTable: "recipes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_recipe_subscription_users_SubscriberId",
                table: "recipe_subscription",
                column: "SubscriberId",
                principalTable: "users",
                principalColumn: "EntryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_subscriptions_users_SubscriberId1",
                table: "user_subscriptions",
                column: "SubscriberId1",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
