﻿using AutoMapper;
using CulinaryRecipes.Common.Exceptions;
using CulinaryRecipes.Common.Validator;
using CulinaryRecipes.Context;
using Microsoft.EntityFrameworkCore;
using CulinaryRecipes.Context.Entities;

namespace CulinaryRecipes.Services.Ingredients;

public class IngredientService : IIngredientService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<CreateIngredientModel> createModelValidator;
    private readonly IModelValidator<UpdateIngredientModel> updateModelValidator;

    public IngredientService(IDbContextFactory<MainDbContext> dbContextFactory, IMapper mapper,
        IModelValidator<CreateIngredientModel> createModelValidator, IModelValidator<UpdateIngredientModel> updateModelValidator)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.createModelValidator = createModelValidator;
        this.updateModelValidator = updateModelValidator;
    }

    public async Task<IEnumerable<IngredientModel>> GetAll()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var ingredients = await context.Ingredients
            .Include(x => x.IngredientsInRecipe)
            .ToListAsync();

        var result = mapper.Map<IEnumerable<IngredientModel>>(ingredients);

        return result;
    }

    public async Task<IngredientModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var ingredient = await context.Ingredients
            .Include(x => x.IngredientsInRecipe)
            .FirstOrDefaultAsync(x => x.Uid == id);

        var result = mapper.Map<IngredientModel>(ingredient);
        return result;
    }

    public async Task<IngredientModel> Create(CreateIngredientModel model)
    {
        await createModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var ingredient = mapper.Map<Ingredient>(model);

        await context.Ingredients.AddAsync(ingredient);
        await context.SaveChangesAsync();

        return mapper.Map<IngredientModel>(ingredient);
    }

    public async Task Update(Guid id, UpdateIngredientModel model)
    {
        await updateModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var ingredient = await context.Ingredients.Where(x => x.Uid == id).FirstOrDefaultAsync();

        ingredient = mapper.Map(model, ingredient);

        context.Ingredients.Update(ingredient);

        await context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var ingredient = await context.Ingredients.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (ingredient == null)
            throw new ProcessException($"Ingredient (ID = {id}) not found.");

        context.Ingredients.Remove(ingredient);

        await context.SaveChangesAsync();
    }
}

