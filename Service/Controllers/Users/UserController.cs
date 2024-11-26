using AutoMapper;
using BL.Users.Entities;
using BL.Users.Exceptions;
using BL.Users.Manager;
using BL.Users.Provider;
using Microsoft.AspNetCore.Http.HttpResults;
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
    [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
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
    [ProducesResponseType(typeof(UsersListResponse), StatusCodes.Status200OK)]
    public IActionResult GetAllUsers()
    {
        var users = _usersProvider.GetUsers();
        return Ok(new UsersListResponse
        {
            Users = users.ToList()
        });
    }
    
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public IActionResult GetUserById(long id)
    {
        try
        {
            var user = _usersProvider.GerUserInfo(id);
            return Ok(user);
        }
        catch (UserNotFoundException e)
        {
            _logger.Error(e.ToString());
            return NotFound(e.Message);
        }
      
    }

    [HttpGet]
    [Route("filter")]
    [ProducesResponseType(typeof(UsersListResponse), StatusCodes.Status200OK)]
    public IActionResult GetFilteredUsers([FromQuery] UserFilter filter)
    {
        var userFilterModel = _mapper.Map<FilterUserModel>(filter);
        var users = _usersProvider.GetUsers(userFilterModel);
        return Ok(new UsersListResponse
        {
            Users = users.ToList()
        });
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IActionResult UpdateUserById([FromBody] UpdateUserModel request, [FromRoute] long id)
    {
        try
        {
            var updateUser = _usersManager.UpdateUser(request, id);
            return Ok(updateUser);

        }
        catch (UserNotFoundException e)
        {
            _logger.Error(e.ToString());
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            _logger.Error(e.ToString());
            return BadRequest(e.Message);
        }

    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public IActionResult DeleteUserById([FromRoute] long id)
    {
        try
        {
            _usersManager.DeleteUser(id);
            return Ok();
        }
        catch (UserNotFoundException e)
        {
            _logger.Error(e.ToString());
            return NotFound(e.Message);
        }
    }
}