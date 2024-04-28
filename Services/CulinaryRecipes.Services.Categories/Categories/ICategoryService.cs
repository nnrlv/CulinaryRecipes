namespace CulinaryRecipes.Services.Categories;

/// <summary>
/// Represents a service for managing categories.
/// </summary>
public interface ICategoryService
{
    /// <summary>
    /// Gets all categories.
    /// </summary>
    /// <returns> The task result contains a collection of category models.</returns>
    Task<IEnumerable<CategoryModel>> GetAll();

    /// <summary>
    /// Gets all categories with caching.
    /// </summary>
    /// <returns>The task result contains a collection of category models.</returns>
    Task<IEnumerable<CategoryModel>> GetAllWithCaching();

    /// <summary>
    /// Gets a category by ID.
    /// </summary>
    /// <param name="id">The ID of the category.</param>
    /// <returns>The task result contains the category model.</returns>
    Task<CategoryModel> GetById(Guid id);

    /// <summary>
    /// Gets a category by ID with caching.
    /// </summary>
    /// <param name="id">The ID of the category.</param>
    /// <returns>The task result contains the category model.</returns>
    Task<CategoryModel> GetByIdWithCaching(Guid id);

    /// <summary>
    /// Creates a new category.
    /// </summary>
    /// <param name="model">The model for creating the category.</param>
    /// <returns>The task result contains the created category model.</returns>
    Task<CategoryModel> Create(CreateCategoryModel model);

    /// <summary>
    /// Updates a category.
    /// </summary>
    /// <param name="id">The ID of the category to update.</param>
    /// <param name="model">The model for updating the category.</param>
    Task Update(Guid id, UpdateCategoryModel model);

    /// <summary>
    /// Deletes a category.
    /// </summary>
    /// <param name="id">The ID of the category to delete.</param>
    Task Delete(Guid id);
}