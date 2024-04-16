using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CulinaryRecipes.Context.Migrations.PgSQL.Migrations
{
    /// <inheritdoc />
    public partial class fixedrecipeentityagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categories_recipes_RecipeId",
                table: "categories");

            migrationBuilder.DropForeignKey(
                name: "FK_recipes_categories_CategoryId",
                table: "recipes");

            migrationBuilder.DropIndex(
                name: "IX_recipes_CategoryId",
                table: "recipes");

            migrationBuilder.DropIndex(
                name: "IX_categories_RecipeId",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "recipes");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "categories");

            migrationBuilder.CreateTable(
                name: "recipes-categories",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "integer", nullable: false),
                    RecipesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipes-categories", x => new { x.CategoriesId, x.RecipesId });
                    table.ForeignKey(
                        name: "FK_recipes-categories_categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_recipes-categories_recipes_RecipesId",
                        column: x => x.RecipesId,
                        principalTable: "recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_recipes-categories_RecipesId",
                table: "recipes-categories",
                column: "RecipesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "recipes-categories");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "recipes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "categories",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_recipes_CategoryId",
                table: "recipes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_categories_RecipeId",
                table: "categories",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_categories_recipes_RecipeId",
                table: "categories",
                column: "RecipeId",
                principalTable: "recipes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_recipes_categories_CategoryId",
                table: "recipes",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id");
        }
    }
}
