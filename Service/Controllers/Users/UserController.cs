using AutoMapper;
using BL.Users.Entities;
using BL.Users.Manager;
using BL.Users.Provider;
using Microsoft.AspNetCore.Mvc;
using Service.Controllers.Users.Entities;
using Service.Controllers.Validation;
namespace Service.Controllers.Users;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly Serilog.ILogger _logger;
    private readonly IMapper _mapper;
    private readonly IUserManager _usersManager;
    private readonly IUserProvider _usersProvider;

    public UsersController(IUserManager usersManager, IUserProvider usersProvider,
        IMapper mapper, Serilog.ILogger logger)
    {
        _usersManager = usersManager;
        _usersProvider = usersProvider;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost]
    public IActionResult RegisterUser([FromBody] RegisterUserRequest request)
    {
        var validationResult = new RegisterUserRequestValidator().Validate(request);
        if (validationResult.IsValid)
        {
            var createUserModel = _mapper.Map<CreateUserModel>(request);
            var userModel = _usersManager.CreateUser(createUserModel);
            return Ok(userModel);
        }
        
        _logger.Error(validationResult.ToString());
        return BadRequest(validationResult.ToString());
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = _usersProvider.GetUsers();
        return Ok(new UsersListResponse
        {
            Users = users.ToList()
        });
    }

    [HttpGet]
    [Route("filter")]
    public IActionResult GetFilteredUsers([FromQuery] UserFilter filter)
    {
        var userFilterModel = _mapper.Map<FilterUserModel>(filter);
        var users = _usersProvider.GetUsers(userFilterModel);
        return Ok(new UsersListResponse
        {
            Users = users.ToList()
        });
    }
}