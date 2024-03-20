namespace CulinaryRecipes.Api.Controllers.Ingredient;

using Microsoft.AspNetCore.Mvc;
using CulinaryRecipes.Services.Ingredients;
using CulinaryRecipes.Services.Logger;
using AutoMapper;
using CulinaryRecipes.Common.Security;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Product")]
[Route("v{version:apiVersion}/[controller]")]
public class IngredientController : Controller
{
    private readonly IMapper mapper;
    private readonly IAppLogger logger;
    private readonly IIngredientService ingredientService;

    public IngredientController(IAppLogger logger, IIngredientService ingredientService, IMapper mapper)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.ingredientService = ingredientService;
    }

    [HttpGet("")]
    [Authorize(Policy = AppScopes.IngredientsRead)]
    public async Task<IEnumerable<IngredientResponse>> GetAll()
    {
        var result = await ingredientService.GetAll();
        return mapper.Map<IEnumerable<IngredientResponse>>(result);
    }

    [HttpGet("{id:Guid}")]
    [Authorize(Policy = AppScopes.IngredientsRead)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await ingredientService.GetById(id);

        if (result == null)
            return NotFound();

        var response = mapper.Map<IngredientResponse>(result);

        return Ok(response);
    }

    [HttpPost("")]
    [Authorize(Policy = AppScopes.IngredientsWrite)]
    public async Task<IngredientResponse> Create(CreateIngredientRequest request)
    {
        var result = await ingredientService.Create(mapper.Map<CreateIngredientModel>(request));

        var response = mapper.Map<IngredientResponse>(result);

        return response;
    }

    [HttpPut("{id:Guid}")]
    [Authorize(Policy = AppScopes.IngredientsWrite)]
    public async Task Update([FromRoute] Guid id, UpdateIngredientRequest request)
    {
        await ingredientService.Update(id, mapper.Map<UpdateIngredientModel>(request));
    }

    [HttpDelete("{id:Guid}")]
    [Authorize(Policy = AppScopes.IngredientsWrite)]
    public async Task Delete([FromRoute] Guid id)
    {
        await ingredientService.Delete(id);
    }
}
