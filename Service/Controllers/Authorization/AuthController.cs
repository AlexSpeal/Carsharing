using BL.Authorization;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Service.Controllers.Authorization;

public class AuthController(IAuthProvider authProvider) : ControllerBase
{
    [HttpGet]
    [Route("login")]
    public async Task<IActionResult> LoginUser([FromQuery] string email, [FromQuery] string password)
    {
        try
        {
            var tokens = await authProvider.AuthorizeUser(email, password);
            return Ok(tokens);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
  
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterUser(string fullName,string login,string email, string password)
    {
        try
        {
            var user = await authProvider.RegisterUser(fullName,login,email, password);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}