namespace CulinaryRecipes.Services.Categories;

public interface ICategoryService
{
    Task<IEnumerable<CategoryModel>> GetAll();

    Task<IEnumerable<CategoryModel>> GetAllWithCaching();

    Task<CategoryModel> GetById(Guid id);

    Task<CategoryModel> GetByIdWithCaching(Guid id);

    Task<CategoryModel> Create(CreateCategoryModel model);

    Task Update(Guid id, UpdateCategoryModel model);

    Task Delete(Guid id);
}