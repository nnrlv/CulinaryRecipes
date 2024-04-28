using AutoMapper;
using CulinaryRecipes.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CulinaryRecipes.Services.Comments;

/// <summary>
/// Represents a model for creating a comment.
/// </summary>
public class CreateCommentModel
{
    /// <summary>
    /// The ID of the user who posted the comment.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// The ID of the recipe the comment belongs to.
    /// </summary>
    public Guid RecipeId { get; set; }

    /// <summary>
    /// The text of the comment.
    /// </summary>
    public string Text { get; set; }
}

public class CreateCommentModelProfile : Profile
{
    public CreateCommentModelProfile()
    {
        CreateMap<CreateCommentModel, Comment>()
            .BeforeMap<CreateCommentModelActions>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.RecipeId, opt => opt.Ignore());
    }

    public class CreateCommentModelActions : IMappingAction<CreateCommentModel, Comment>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public CreateCommentModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(CreateCommentModel source, Comment destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var user = db.Users.FirstOrDefault(x => x.Id == source.UserId);

            var recipe = db.Recipes.FirstOrDefault(x => x.Uid == source.RecipeId);

            destination.UserId = user.EntryId;
            destination.RecipeId = recipe.Id;
        }
    }

}

public class CreateModelValidator : AbstractValidator<CreateCommentModel>
{
    public CreateModelValidator(IDbContextFactory<MainDbContext> contextFactory)
    {
        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Text is required")
            .MaximumLength(500).WithMessage("Text can't be longer than 500 symbols");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User is required")
            .Must(id =>
            {
                using var context = contextFactory.CreateDbContext();
                var found = context.Users.Any(a => a.Id == id);
                return found;
            }).WithMessage("User not found");

        RuleFor(x => x.RecipeId)
            .NotEmpty().WithMessage("Recipe is required")
            .Must(id =>
            {
                using var context = contextFactory.CreateDbContext();
                var found = context.Recipes.Any(a => a.Uid == id);
                return found;
            }).WithMessage("Recipe not found");
    }
}