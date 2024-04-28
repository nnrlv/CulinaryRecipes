namespace CulinaryRecipes.Api.Controllers.Category;

using AutoMapper;
using CulinaryRecipes.Common.Security;
using CulinaryRecipes.Services.Categories;
using CulinaryRecipes.Services.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller for managing categories.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]")]
public class CategoryController : Controller
{
    private readonly IMapper mapper;
    private readonly IAppLogger logger;
    private readonly ICategoryService categoryService;

    /// <summary>
    /// Initializes a new instance of the CategoryController class.
    /// </summary>
    /// <param name="logger">The logger service.</param>
    /// <param name="categoryService">The category service.</param>
    /// <param name="mapper">The mapper service.</param>
    public CategoryController(IAppLogger logger, ICategoryService categoryService, IMapper mapper)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.categoryService = categoryService;
    }

    /// <summary>
    /// Gets all categories without caching.
    /// </summary>
    /// <returns>A collection of category responses.</returns>
    [HttpGet("uncached")]
    [Authorize(Policy = AppScopes.CategoriesRead)]
    public async Task<IEnumerable<CategoryResponse>> GetAll()
    {
        var result = await categoryService.GetAll();
        return mapper.Map<IEnumerable<CategoryResponse>>(result);
    }

    /// <summary>
    /// Gets all categories with caching.
    /// </summary>
    /// <returns>A collection of category responses.</returns>
    [HttpGet("cached")]
    [Authorize(Policy = AppScopes.CategoriesRead)]
    public async Task<IEnumerable<CategoryResponse>> GetAllWithCaching()
    {
        var result = await categoryService.GetAllWithCaching();
        return mapper.Map<IEnumerable<CategoryResponse>>(result);
    }

    /// <summary>
    /// Gets a category by ID without caching.
    /// </summary>
    /// <param name="id">The ID of the category.</param>
    /// <returns>The category response.</returns>
    [HttpGet("uncached/{id:Guid}")]
    [Authorize(Policy = AppScopes.CategoriesRead)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await categoryService.GetById(id);

        if (result == null)
            return NotFound();

        var response = mapper.Map<CategoryResponse>(result);

        return Ok(response);
    }

    /// <summary>
    /// Gets a category by ID with caching.
    /// </summary>
    /// <param name="id">The ID of the category.</param>
    /// <returns>The category response.</returns>
    [HttpGet("cached/{id:Guid}")]
    [Authorize(Policy = AppScopes.CategoriesRead)]
    public async Task<IActionResult> GetWithCaching([FromRoute] Guid id)
    {
        var result = await categoryService.GetByIdWithCaching(id);

        if (result == null)
            return NotFound();

        var response = mapper.Map<CategoryResponse>(result);

        return Ok(response);
    }

    /// <summary>
    /// Creates a new category.
    /// </summary>
    /// <param name="request">The request to create a category.</param>
    /// <returns>The created category response.</returns>
    [HttpPost("")]
    [Authorize(Policy = AppScopes.CategoriesWrite)]
    public async Task<CategoryResponse> Create([FromQuery] CreateCategoryRequest request)
    {
        var result = await categoryService.Create(mapper.Map<CreateCategoryModel>(request));

        var response = mapper.Map<CategoryResponse>(result);

        return response;
    }

    /// <summary>
    /// Updates a category by ID.
    /// </summary>
    /// <param name="id">The ID of the category.</param>
    /// <param name="request">The request to update the category.</param>
    [HttpPut("{id:Guid}")]
    [Authorize(Policy = AppScopes.CategoriesWrite)]
    public async Task Update([FromRoute] Guid id, [FromQuery] UpdateCategoryRequest request)
    {
        await categoryService.Update(id, mapper.Map<UpdateCategoryModel>(request));
    }

    /// <summary>
    /// Deletes a category by ID.
    /// </summary>
    /// <param name="id">The ID of the category.</param>
    [HttpDelete("{id:Guid}")]
    [Authorize(Policy = AppScopes.CategoriesWrite)]
    public async Task Delete([FromRoute] Guid id)
    {
        await categoryService.Delete(id);
    }
}
