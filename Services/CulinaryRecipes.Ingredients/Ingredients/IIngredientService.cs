namespace CulinaryRecipes.Services.Ingredients;

public interface IIngredientService
{
    Task<IEnumerable<IngredientModel>> GetAll();

    Task<IEnumerable<IngredientModel>> GetAllWithCaching();

    Task<IngredientModel> GetById(Guid id);

    Task<IngredientModel> GetByIdWithCaching(Guid id);

    Task<IngredientModel> Create(CreateIngredientModel model);

    Task Update(Guid id, UpdateIngredientModel model);

    Task Delete(Guid id);
}