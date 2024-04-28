namespace CulinaryRecipes.Api.Controllers.Subscription;

using AutoMapper;
using CulinaryRecipes.Common.Security;
using CulinaryRecipes.Services.Logger;
using CulinaryRecipes.Services.Subscriptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]")]
public class SubscriptionController : Controller
{
    private readonly IMapper mapper;
    private readonly IAppLogger logger;
    private readonly ISubscriptionService subscriptionService;

    public SubscriptionController(IAppLogger logger, ISubscriptionService subscriptionService, IMapper mapper)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.subscriptionService = subscriptionService;
    }

    /// <summary>
    /// Subscribes a user to another user.
    /// </summary>
    [HttpPost("user")]
    [Authorize(Policy = AppScopes.Subscribe)]
    public async Task<ActionResult> SubscribeToUser([FromQuery] Guid subscriberId, [FromQuery] Guid authorId)
    {
        await subscriptionService.SubscribeToUser(subscriberId, authorId);
        return Ok();
    }

    /// <summary>
    /// Unsubscribes a user from another user.
    /// </summary>
    [HttpDelete("user")]
    [Authorize(Policy = AppScopes.Subscribe)]
    public async Task<ActionResult> UnsuscribeFromUser([FromQuery] Guid subscriberId, [FromQuery] Guid authorId)
    {
        await subscriptionService.UnsubscribeFromUser(subscriberId, authorId);
        return Ok();
    }

    /// <summary>
    /// Subscribes a user to a recipe.
    /// </summary>
    [HttpPost("recipe")]
    [Authorize(Policy = AppScopes.Subscribe)]
    public async Task<ActionResult> SubscribeToRecipe([FromQuery] Guid subscriberId, [FromQuery] Guid recipeId)
    {
        await subscriptionService.SubscribeToRecipe(subscriberId, recipeId);
        return Ok();
    }
    /// <summary>
    /// Unsubscribes a user from a recipe.
    /// </summary>
    [HttpDelete("recipe")]
    [Authorize(Policy = AppScopes.Subscribe)]
    public async Task<ActionResult> UnsuscribeFromRecipe([FromQuery] Guid subscriberId, [FromQuery] Guid recipeId)
    {
        await subscriptionService.UnsubscribeFromRecipe(subscriberId, recipeId);
        return Ok();
    }
}
