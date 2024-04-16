using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CulinaryRecipes.Context.Migrations.PgSQL.Migrations
{
    /// <inheritdoc />
    public partial class addedintermediateentityforrecipeandcategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "recipes-categories");

            migrationBuilder.CreateTable(
                name: "recipes_in_categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RecipeId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipes_in_categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_recipes_in_categories_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_recipes_in_categories_recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "recipes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_recipes_in_categories_CategoryId",
                table: "recipes_in_categories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_recipes_in_categories_RecipeId",
                table: "recipes_in_categories",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_recipes_in_categories_Uid",
                table: "recipes_in_categories",
                column: "Uid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "recipes_in_categories");

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
    }
}
