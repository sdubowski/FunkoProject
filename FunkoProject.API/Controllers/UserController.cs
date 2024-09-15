using FunkoProject.Data.Entities;
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
        public User GetById(string id)
        {
            var user = _userService.GetUserById(id);
            return user;
        }
    }