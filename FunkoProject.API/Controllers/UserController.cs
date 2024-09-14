using FunkoProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace FunkoProject.Controllers;

[ApiController]
[Route("api/Users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Route("GetUser/{id}")]
    public IActionResult GetById(int id)
    {
        var user = _userService.GetUser(id);
        return Ok(user);
    }
}