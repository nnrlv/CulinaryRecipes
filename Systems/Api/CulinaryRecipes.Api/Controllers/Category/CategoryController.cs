namespace CulinaryRecipes.Api.Controllers.Category;

using Microsoft.AspNetCore.Mvc;
using CulinaryRecipes.Services.Categories;
using CulinaryRecipes.Services.Logger;
using AutoMapper;
using CulinaryRecipes.Common.Security;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]")]
public class CategoryController : Controller
{
    private readonly IMapper mapper;
    private readonly IAppLogger logger;
    private readonly ICategoryService categoryService;

    public CategoryController(IAppLogger logger, ICategoryService categoryService, IMapper mapper)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.categoryService = categoryService;
    }

    [HttpGet("uncached")]
    [Authorize(Policy = AppScopes.CategoriesRead)]
    public async Task<IEnumerable<CategoryResponse>> GetAll()
    {
        var result = await categoryService.GetAll();
        return mapper.Map<IEnumerable<CategoryResponse>>(result);
    }

    [HttpGet("cached")]
    [Authorize(Policy = AppScopes.CategoriesRead)]
    public async Task<IEnumerable<CategoryResponse>> GetAllWithCaching()
    {
        var result = await categoryService.GetAllWithCaching();
        return mapper.Map<IEnumerable<CategoryResponse>>(result);
    }

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

    [HttpPost("")]
    [Authorize(Policy = AppScopes.CategoriesWrite)]
    public async Task<CategoryResponse> Create([FromQuery] CreateCategoryRequest request)
    {
        var result = await categoryService.Create(mapper.Map<CreateCategoryModel>(request));

        var response = mapper.Map<CategoryResponse>(result);

        return response;
    }

    [HttpPut("{id:Guid}")]
    [Authorize(Policy = AppScopes.CategoriesWrite)]
    public async Task Update([FromRoute] Guid id, [FromQuery] UpdateCategoryRequest request)
    {
        await categoryService.Update(id, mapper.Map<UpdateCategoryModel>(request));
    }

    [HttpDelete("{id:Guid}")]
    [Authorize(Policy = AppScopes.CategoriesWrite)]
    public async Task Delete([FromRoute] Guid id)
    {
        await categoryService.Delete(id);
    }
}
