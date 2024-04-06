namespace CulinaryRecipes.Api.Controllers.UserAccounts;

using AutoMapper;
using CulinaryRecipes.Common.Security;
using CulinaryRecipes.Services.UserAccounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPost("")]
    public async Task<UserAccountResponse> Register([FromQuery] CreateUserAccountRequest request)
    {
        var result = await userAccountService.Create(mapper.Map<CreateUserAccountModel>(request));

        var response = mapper.Map<UserAccountResponse>(result);

        return response;
    }

    [HttpPost("request-email-confirmation")]
    public async Task RequestEmailConfirmation([FromQuery] string email)
    {
        await userAccountService.RequestEmailConfirmation(email);
    }

    [HttpPut("confirm-email")]
    public async Task ConfirmEmail([FromQuery] string token, [FromQuery] string email)
    {
        await userAccountService.ConfirmEmail(token, email);
    }

    [HttpGet("")]
    [Authorize(Policy = AppScopes.UserAccountsRead)]
    public async Task<IEnumerable<UserAccountResponse>> GetAll()
    {
        var result = await userAccountService.GetAll();
        return mapper.Map<IEnumerable<UserAccountResponse>>(result);
    }

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