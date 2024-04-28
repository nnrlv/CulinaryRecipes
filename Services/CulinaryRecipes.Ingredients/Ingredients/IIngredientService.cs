namespace CulinaryRecipes.Services.Ingredients;

/// <summary>
/// Interface for the ingredient service.
/// </summary>
public interface IIngredientService
{
    /// <summary>
    /// Gets all ingredients.
    /// </summary>
    /// <returns>Contains a collection of IngredientModel.</returns>
    Task<IEnumerable<IngredientModel>> GetAll();

    /// <summary>
    /// Gets all ingredients with caching.
    /// </summary>
    /// <returns>Contains a collection of IngredientModel.</returns>
    Task<IEnumerable<IngredientModel>> GetAllWithCaching();

    /// <summary>
    /// Gets specific ingredient by id.
    /// </summary>
    /// <returns>The task result contains the ingredient model.</returns>
    Task<IngredientModel> GetById(Guid id);

    /// <summary>
    /// Gets specific ingredient by id with caching.
    /// </summary>
    /// <returns>The task result contains the ingredient model.</returns>
    Task<IngredientModel> GetByIdWithCaching(Guid id);

    /// <summary>
    /// Creates a new ingredient based on the specified model.
    /// </summary>
    /// <param name="model">The model containing information about the ingredient to create.</param>
    /// <returns>The created ingredient model.</returns>
    Task<IngredientModel> Create(CreateIngredientModel model);

    /// <summary>
    /// Updates an existing ingredient with the specified ID using the information from the provided model.
    /// </summary>
    /// <param name="id">The ID of the ingredient to update.</param>
    /// <param name="model">The model containing the updated information for the ingredient.</param>
    Task Update(Guid id, UpdateIngredientModel model);

    /// <summary>
    /// Deletes the ingredient with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the ingredient to delete.</param>
    Task Delete(Guid id);
}