namespace CulinaryRecipes.Api.Controllers.Ingredient;

using Microsoft.AspNetCore.Mvc;
using CulinaryRecipes.Services.Ingredients;
using CulinaryRecipes.Services.Logger;
using AutoMapper;
using CulinaryRecipes.Common.Security;
using Microsoft.AspNetCore.Authorization;

/// <summary>
/// Controller for managing ingredients.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
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

    /// <summary>
    /// Gets all ingredients with caching.
    /// </summary>
    /// <returns>A collection of ingredient responses.</returns>
    [HttpGet("cached")]
    [Authorize(Policy = AppScopes.IngredientsRead)]
    public async Task<IEnumerable<IngredientResponse>> GetAllWithCaching()
    {
        var result = await ingredientService.GetAllWithCaching();
        return mapper.Map<IEnumerable<IngredientResponse>>(result);
    }

    /// <summary>
    /// Gets all ingredients without caching.
    /// </summary>
    /// <returns>A collection of ingredient responses.</returns>
    [HttpGet("uncached")]
    [Authorize(Policy = AppScopes.IngredientsRead)]
    public async Task<IEnumerable<IngredientResponse>> GetAll()
    {
        var result = await ingredientService.GetAll();
        return mapper.Map<IEnumerable<IngredientResponse>>(result);
    }

    /// <summary>
    /// Gets an ingredient by ID with caching.
    /// </summary>
    /// <param name="id">The ID of the ingredient.</param>
    /// <returns>The ingredient response.</returns>
    [HttpGet("cached/{id:Guid}")]
    [Authorize(Policy = AppScopes.IngredientsRead)]
    public async Task<IActionResult> GetWithCaching([FromRoute] Guid id)
    {
        var result = await ingredientService.GetByIdWithCaching(id);

        if (result == null)
            return NotFound();

        var response = mapper.Map<IngredientResponse>(result);

        return Ok(response);
    }

    /// <summary>
    /// Gets an ingredient by ID without caching.
    /// </summary>
    /// <param name="id">The ID of the ingredient.</param>
    /// <returns>The ingredient response.</returns>
    [HttpGet("uncached/{id:Guid}")]
    [Authorize(Policy = AppScopes.IngredientsRead)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await ingredientService.GetById(id);

        if (result == null)
            return NotFound();

        var response = mapper.Map<IngredientResponse>(result);

        return Ok(response);
    }

    /// <summary>
    /// Creates a new ingredient.
    /// </summary>
    /// <param name="request">The request to create an ingredient.</param>
    /// <returns>The created ingredient response.</returns>
    [HttpPost("")]
    [Authorize(Policy = AppScopes.IngredientsWrite)]
    public async Task<IngredientResponse> Create(CreateIngredientRequest request)
    {
        var result = await ingredientService.Create(mapper.Map<CreateIngredientModel>(request));

        var response = mapper.Map<IngredientResponse>(result);

        return response;
    }

    /// <summary>
    /// Updates an ingredient by ID.
    /// </summary>
    /// <param name="id">The ID of the ingredient.</param>
    /// <param name="request">The request to update the ingredient.</param>
    [HttpPut("{id:Guid}")]
    [Authorize(Policy = AppScopes.IngredientsWrite)]
    public async Task Update([FromRoute] Guid id, UpdateIngredientRequest request)
    {
        await ingredientService.Update(id, mapper.Map<UpdateIngredientModel>(request));
    }

    /// <summary>
    /// Deletes an ingredient by ID.
    /// </summary>
    /// <param name="id">The ID of the ingredient.</param>
    [HttpDelete("{id:Guid}")]
    [Authorize(Policy = AppScopes.IngredientsWrite)]
    public async Task Delete([FromRoute] Guid id)
    {
        await ingredientService.Delete(id);
    }
}
