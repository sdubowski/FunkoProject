using FunkoProject.Models;
using FunkoProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FunkoProject.Controllers;

[ApiController]
[Route("api/User")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("GetUserById/{id}")]
    public IActionResult GetById(int id)
    {
        var user = _userService.GetUserById(id);
        return Ok(user);
    }

    [HttpPost]
    [Route("AddFriend")]
    public IActionResult AddFriend([FromBody] int userId, int friendId)
    {
        var serviceMessage = _userService.AddFriend(userId, friendId);

        if (!serviceMessage.Success)
        {
            return BadRequest(serviceMessage);
        }

        return Ok();
    }


    // [HttpDelete]
    // [Route("Delete/{friendId:int}")]
    // public IActionResult DeleteFriend(int friendId)
    // {
    //     _figuresService.DeleteFigure(Id);
    //     return Ok();
    // }
}