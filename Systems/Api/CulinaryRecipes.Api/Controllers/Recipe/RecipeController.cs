namespace CulinaryRecipes.Api.Controllers.Recipe;

using AutoMapper;
using CulinaryRecipes.Services.Logger;
using CulinaryRecipes.Services.Recipes;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("uncached")]
    public async Task<IEnumerable<ShortRecipeResponse>> GetAllShortRecipes()
    {
        var result = await recipeService.GetAllShortRecipes();
        return mapper.Map<IEnumerable<ShortRecipeResponse>>(result);
    }

    [HttpGet("cached")]
    public async Task<IEnumerable<ShortRecipeResponse>> GetAllShortRecipesWithCaching()
    {
        var result = await recipeService.GetAllShortRecipesWithCaching();
        return mapper.Map<IEnumerable<ShortRecipeResponse>>(result);
    }

    [HttpGet("uncached/{id:Guid}")]
    public async Task<IActionResult> GetShortRecipe([FromRoute] Guid id)
    {
        var result = await recipeService.GetShortRecipeById(id);

        if (result == null)
            return NotFound();

        return Ok(mapper.Map<ShortRecipeResponse>(result));
    }

    [HttpGet("cached/{id:Guid}")]
    public async Task<IActionResult> GetShortRecipeWithCaching([FromRoute] Guid id)
    {
        var result = await recipeService.GetShortRecipeByIdWithCaching(id);

        if (result == null)
            return NotFound();

        return Ok(mapper.Map<ShortRecipeResponse>(result));
    }

    [HttpGet("{id:Guid}/ingredients")]
    public async Task<IEnumerable<IngredientInRecipeModel>> GetIngredientsOfRecipe([FromRoute] Guid id)
    {
        var result = await recipeService.GetIngredientsOfRecipe(id);
        return result;
    }

    [HttpGet("uncached/full-info/{id:Guid}")]
    public async Task<IActionResult> GetFullRecipeById([FromRoute] Guid id)
    {
        var result = await recipeService.GetFullRecipeById(id);

        if (result == null)
            return NotFound();

        return Ok(mapper.Map<FullRecipeResponse>(result));
    }

    [HttpGet("cached/full-info/{id:Guid}")]
    public async Task<IActionResult> GetFullRecipeByIdWithCaching([FromRoute] Guid id)
    {
        var result = await recipeService.GetFullRecipeByIdWithCaching(id);

        if (result == null)
            return NotFound();

        return Ok(mapper.Map<FullRecipeResponse>(result));
    }

    [HttpPost("")]
    public async Task<ShortRecipeResponse> Create([FromQuery] Guid userId, [FromQuery] CreateRecipeRequest model)
    {
        var result = await recipeService.Create(mapper.Map<CreateRecipeModel>(model), userId.ToString());

        return mapper.Map<ShortRecipeResponse>(result);
    }

    [HttpPost("{recipeId:Guid}/ingredients/{ingredientId:Guid}")]
    public async Task AddIngredientInRecipe([FromRoute] Guid recipeId, [FromRoute] Guid ingredientId)
    {
        await recipeService.AddIngredientInRecipe(recipeId, ingredientId);
    }

    [HttpPost("{recipeId:Guid}/categories/{categoryId:Guid}")]
    public async Task AddCategoryToRecipe([FromRoute] Guid recipeId, [FromRoute] Guid categoryId)
    {
        await recipeService.AddCategoryToRecipe(recipeId, categoryId);
    }

    [HttpPut("")]
    public async Task Update([FromQuery] Guid id, [FromQuery] UpdateRecipeRequest model)
    {
        await recipeService.Update(id, mapper.Map<UpdateRecipeModel>(model));
    }

    [HttpDelete("")]
    public async Task Delete([FromQuery] Guid id)
    {
        await recipeService.Delete(id);
    }
}
