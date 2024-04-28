namespace CulinaryRecipes.Api.Controllers.Recipe;

using AutoMapper;
using CulinaryRecipes.Common.Security;
using CulinaryRecipes.Services.Logger;
using CulinaryRecipes.Services.Recipes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller for managing recipes.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]")]
public class RecipeController : Controller
{
    private readonly IMapper mapper;
    private readonly IAppLogger logger;
    private readonly IRecipeService recipeService;

    public RecipeController(IAppLogger logger, IRecipeService recipeService, IMapper mapper)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.recipeService = recipeService;
    }

    /// <summary>
    /// Gets all short recipes without caching.
    /// </summary>
    /// <returns>A collection of short recipe responses.</returns>
    [HttpGet("uncached")]
    public async Task<IEnumerable<ShortRecipeResponse>> GetAllShortRecipes()
    {
        var result = await recipeService.GetAllShortRecipes();
        return mapper.Map<IEnumerable<ShortRecipeResponse>>(result);
    }

    /// <summary>
    /// Gets all short recipes with caching.
    /// </summary>
    /// <returns>A collection of short recipe responses.</returns>
    [HttpGet("cached")]
    public async Task<IEnumerable<ShortRecipeResponse>> GetAllShortRecipesWithCaching()
    {
        var result = await recipeService.GetAllShortRecipesWithCaching();
        return mapper.Map<IEnumerable<ShortRecipeResponse>>(result);
    }

    /// <summary>
    /// Gets filtered short recipes with caching based on specified criteria.
    /// </summary>
    /// <param name="userName">The user name to filter by.</param>
    /// <param name="minPreparationTime">The minimum preparation time to filter by.</param>
    /// <param name="maxPreparationTime">The maximum preparation time to filter by.</param>
    /// <param name="minCookingTime">The minimum cooking time to filter by.</param>
    /// <param name="maxCookingTime">The maximum cooking time to filter by.</param>
    /// <param name="categoryName">The category name to filter by.</param>
    /// <returns>A collection of filtered short recipe responses.</returns>
    [HttpGet("filtered")]
    public async Task<IEnumerable<ShortRecipeResponse>> GetFilteredShortRecipes(
        [FromQuery] string? userName,
        [FromQuery] float? minPreparationTime,
        [FromQuery] float? maxPreparationTime,
        [FromQuery] float? minCookingTime,
        [FromQuery] float? maxCookingTime,
        [FromQuery] string? categoryName)
    {
        var result = await recipeService.GetFilteredShortRecipesWithCaching(
            userName,
            minPreparationTime,
            maxPreparationTime,
            minCookingTime,
            maxCookingTime,
            categoryName);
        return mapper.Map<IEnumerable<ShortRecipeResponse>>(result);
    }

    /// <summary>
    /// Gets a short recipe by ID without caching.
    /// </summary>
    /// <param name="id">The ID of the recipe.</param>
    /// <returns>The short recipe response.</returns>
    [HttpGet("uncached/{id:Guid}")]
    public async Task<IActionResult> GetShortRecipe([FromRoute] Guid id)
    {
        var result = await recipeService.GetShortRecipeById(id);

        if (result == null)
            return NotFound();

        return Ok(mapper.Map<ShortRecipeResponse>(result));
    }

    /// <summary>
    /// Gets a short recipe by ID with caching.
    /// </summary>
    /// <param name="id">The ID of the recipe.</param>
    /// <returns>The short recipe response.</returns>
    [HttpGet("cached/{id:Guid}")]
    public async Task<IActionResult> GetShortRecipeWithCaching([FromRoute] Guid id)
    {
        var result = await recipeService.GetShortRecipeByIdWithCaching(id);

        if (result == null)
            return NotFound();

        return Ok(mapper.Map<ShortRecipeResponse>(result));
    }

    /// <summary>
    /// Gets the ingredients of a recipe by recipe ID.
    /// </summary>
    /// <param name="id">The ID of the recipe.</param>
    /// <returns>The ingredients of the recipe.</returns>
    [HttpGet("{id:Guid}/ingredients")]
    public async Task<IEnumerable<IngredientInRecipeModel>> GetIngredientsOfRecipe([FromRoute] Guid id)
    {
        var result = await recipeService.GetIngredientsOfRecipe(id);
        return result;
    }

    /// <summary>
    /// Gets the full information of a recipe by ID without caching.
    /// </summary>
    /// <param name="id">The ID of the recipe.</param>
    /// <returns>The full recipe response.</returns>
    [HttpGet("uncached/full-info/{id:Guid}")]
    public async Task<IActionResult> GetFullRecipeById([FromRoute] Guid id)
    {
        var result = await recipeService.GetFullRecipeById(id);

        if (result == null)
            return NotFound();

        return Ok(mapper.Map<FullRecipeResponse>(result));
    }

    /// <summary>
    /// Gets the full information of a recipe by ID with caching.
    /// </summary>
    /// <param name="id">The ID of the recipe.</param>
    /// <returns>The full recipe response.</returns>
    [HttpGet("cached/full-info/{id:Guid}")]
    public async Task<IActionResult> GetFullRecipeByIdWithCaching([FromRoute] Guid id)
    {
        var result = await recipeService.GetFullRecipeByIdWithCaching(id);

        if (result == null)
            return NotFound();

        return Ok(mapper.Map<FullRecipeResponse>(result));
    }

    /// <summary>
    /// Creates a new recipe.
    /// </summary>
    /// <param name="userId">The ID of the user creating the recipe.</param>
    /// <param name="model">The model containing the details of the recipe to create.</param>
    /// <returns>The created short recipe response.</returns>
    [HttpPost("")]
    [Authorize(Policy = AppScopes.RecipesWrite)]
    public async Task<ShortRecipeResponse> Create([FromQuery] Guid userId, [FromQuery] CreateRecipeRequest model)
    {
        var result = await recipeService.Create(mapper.Map<CreateRecipeModel>(model), userId.ToString());

        return mapper.Map<ShortRecipeResponse>(result);
    }

    /// <summary>
    /// Adds an ingredient to a recipe.
    /// </summary>
    /// <param name="recipeId">The ID of the recipe.</param>
    /// <param name="ingredientId">The ID of the ingredient to add.</param>
    [HttpPost("{recipeId:Guid}/ingredients/{ingredientId:Guid}")]
    [Authorize(Policy = AppScopes.RecipesWrite)]
    public async Task AddIngredientInRecipe([FromRoute] Guid recipeId, [FromRoute] Guid ingredientId)
    {
        await recipeService.AddIngredientInRecipe(recipeId, ingredientId);
    }
    /// <summary>
    /// Adds a category to a recipe.
    /// </summary>
    /// <param name="recipeId">The ID of the recipe.</param>
    /// <param name="categoryId">The ID of the category to add.</param>
    [HttpPost("{recipeId:Guid}/categories/{categoryId:Guid}")]
    [Authorize(Policy = AppScopes.RecipesWrite)]
    public async Task AddCategoryToRecipe([FromRoute] Guid recipeId, [FromRoute] Guid categoryId)
    {
        await recipeService.AddCategoryToRecipe(recipeId, categoryId);
    }

    /// <summary>
    /// Updates a recipe.
    /// </summary>
    /// <param name="id">The ID of the recipe to update.</param>
    /// <param name="model">The model containing the updated details of the recipe.</param>
    [HttpPut("")]
    [Authorize(Policy = AppScopes.RecipesWrite)]
    public async Task Update([FromQuery] Guid id, [FromQuery] UpdateRecipeRequest model)
    {
        await recipeService.Update(id, mapper.Map<UpdateRecipeModel>(model));
    }

    /// <summary>
    /// Deletes a recipe.
    /// </summary>
    /// <param name="id">The ID of the recipe to delete.</param>
    [HttpDelete("")]
    [Authorize(Policy = AppScopes.RecipesWrite)]
    public async Task Delete([FromQuery] Guid id)
    {
        await recipeService.Delete(id);
    }
}
