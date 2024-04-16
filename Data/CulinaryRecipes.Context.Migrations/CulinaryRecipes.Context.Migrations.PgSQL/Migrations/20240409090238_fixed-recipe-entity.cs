using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CulinaryRecipes.Context.Migrations.PgSQL.Migrations
{
    /// <inheritdoc />
    public partial class fixedrecipeentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recipes_ingredients_IngredientId",
                table: "recipes");

            migrationBuilder.DropIndex(
                name: "IX_recipes_IngredientId",
                table: "recipes");

            migrationBuilder.DropColumn(
                name: "IngredientId",
                table: "recipes");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "recipes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "recipes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IngredientId",
                table: "recipes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_recipes_IngredientId",
                table: "recipes",
                column: "IngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_recipes_ingredients_IngredientId",
                table: "recipes",
                column: "IngredientId",
                principalTable: "ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
