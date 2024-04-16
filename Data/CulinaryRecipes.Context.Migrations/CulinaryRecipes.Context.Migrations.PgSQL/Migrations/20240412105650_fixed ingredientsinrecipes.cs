using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CulinaryRecipes.Context.Migrations.PgSQL.Migrations
{
    /// <inheritdoc />
    public partial class fixedingredientsinrecipes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ingredients_in_recipes",
                table: "ingredients_in_recipes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ingredients_in_recipes",
                table: "ingredients_in_recipes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ingredients_in_recipes_IngredientId",
                table: "ingredients_in_recipes",
                column: "IngredientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ingredients_in_recipes",
                table: "ingredients_in_recipes");

            migrationBuilder.DropIndex(
                name: "IX_ingredients_in_recipes_IngredientId",
                table: "ingredients_in_recipes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ingredients_in_recipes",
                table: "ingredients_in_recipes",
                columns: new[] { "IngredientId", "RecipeId" });
        }
    }
}
