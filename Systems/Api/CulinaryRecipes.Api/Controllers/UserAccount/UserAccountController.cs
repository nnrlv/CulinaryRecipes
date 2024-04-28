namespace CulinaryRecipes.Api.Controllers.UserAccounts;

using AutoMapper;
using CulinaryRecipes.Common.Security;
using CulinaryRecipes.Services.UserAccounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller for managing user accounts.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]")]
public class AccountsController : Controller
{
    private readonly IMapper mapper;
    private readonly ILogger<AccountsController> logger;
    private readonly IUserAccountService userAccountService;

    public AccountsController(IMapper mapper, ILogger<AccountsController> logger, IUserAccountService userAccountService)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.userAccountService = userAccountService;
    }

    /// <summary>
    /// Registers a new user account.
    /// </summary>
    /// <param name="request">The request containing user account details.</param>
    /// <returns>The newly created user account.</returns>
    [HttpPost("")]
    public async Task<UserAccountResponse> Register([FromQuery] CreateUserAccountRequest request)
    {
        var result = await userAccountService.Create(mapper.Map<CreateUserAccountModel>(request));

        var response = mapper.Map<UserAccountResponse>(result);

        return response;
    }

    /// <summary>
    /// Requests email confirmation for a user account.
    /// </summary>
    /// <param name="email">The email address of the user.</param>
    [HttpPost("request-email-confirmation")]
    public async Task RequestEmailConfirmation([FromQuery] string email)
    {
        await userAccountService.RequestEmailConfirmation(email);
    }

    /// <summary>
    /// Confirms the email address for a user account.
    /// </summary>
    /// <param name="token">The confirmation token.</param>
    /// <param name="email">The email address of the user.</param>
    [HttpPut("confirm-email")]
    public async Task ConfirmEmail([FromQuery] string token, [FromQuery] string email)
    {
        await userAccountService.ConfirmEmail(token, email);
    }

    /// <summary>
    /// Retrieves all user accounts.
    /// </summary>
    /// <returns>A collection of user account responses.</returns>
    [HttpGet("")]
    [Authorize(Policy = AppScopes.UserAccountsRead)]
    public async Task<IEnumerable<UserAccountResponse>> GetAll()
    {
        var result = await userAccountService.GetAll();
        return mapper.Map<IEnumerable<UserAccountResponse>>(result);
    }

    /// <summary>
    /// Retrieves a user account by ID.
    /// </summary>
    /// <param name="id">The ID of the user account to retrieve.</param>
    /// <returns>The user account response.</returns>
    [HttpGet("{id:Guid}")]
    [Authorize(Policy = AppScopes.UserAccountsRead)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await userAccountService.GetById(id);

        if (result == null)
            return NotFound();

        var response = mapper.Map<UserAccountResponse>(result);

        return Ok(response);
    }
}